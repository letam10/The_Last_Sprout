using System;
using System.Collections.Generic;
using System.IO;
using TheLastSprout.Core;
using UnityEngine;

namespace TheLastSprout.Save
{
    public class SaveManager
    {
        private SaveRegistry _registry;
        private EventBus _eventBus;
        private AtomicSaveWriter _atomicWriter;
        private BackupSaveHandler _backupHandler;
        private List<ISaveMigration> _migrations;
        
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
            _registry = new SaveRegistry();
            _atomicWriter = new AtomicSaveWriter();
            _backupHandler = new BackupSaveHandler();
            _migrations = new List<ISaveMigration>
            {
                new SaveMigration_v0_1_0_to_v0_2_0()
            };
        }

        public void RegisterSaveable(ISaveable saveable) => _registry.Register(saveable);
        public void UnregisterSaveable(ISaveable saveable) => _registry.Unregister(saveable);

        public SaveOperationResult Save(int slot)
        {
            // Example GameState check
            var stateMachine = GameBootstrapper.Instance.Registry.Get<GameStateMachine>();
            if (stateMachine != null)
            {
                if (stateMachine.CurrentState == GameState.BossFight || 
                    stateMachine.CurrentState == GameState.Cutscene ||
                    stateMachine.CurrentState == GameState.Saving)
                {
                    Debug.LogWarning("Cannot save in current game state.");
                    return SaveOperationResult.Error("Cannot save in current state", SaveValidationResult.Valid);
                }
            }

            SaveGameData data = new SaveGameData();
            data.metadata.slotIndex = slot;
            data.metadata.saveVersion = SaveVersion.Current;
            data.metadata.timestampUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            
            _registry.CaptureAll(data);

            string jsonToHash = SaveChecksumUtility.GetJsonForChecksumCalculation(data);
            data.metadata.checksum = SaveChecksumUtility.CalculateChecksum(jsonToHash);

            string finalJson = JsonUtility.ToJson(data, true);
            string path = GetSaveFilePath(slot);

            _backupHandler.RotateBackups(path);
            bool writeSuccess = _atomicWriter.WriteSafely(path, finalJson);

            if (writeSuccess)
            {
                Debug.Log($"Save slot {slot} successful.");
                return SaveOperationResult.Ok();
            }
            else
            {
                Debug.LogError($"Save slot {slot} failed during atomic write.");
                return SaveOperationResult.Error("IO Exception during save", SaveValidationResult.Valid);
            }
        }

        public SaveOperationResult Load(int slot)
        {
            string path = GetSaveFilePath(slot);
            string json = null;
            bool recovered = false;

            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            
            if (string.IsNullOrEmpty(json) || !ValidateJsonIntegrity(json))
            {
                Debug.LogWarning($"Save {slot} corrupted or missing. Trying backup...");
                json = _backupHandler.TryLoadNewestValidBackup(path);
                recovered = true;
            }

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError($"Failed to load save {slot} and no valid backups found.");
                return SaveOperationResult.Error("No valid save data found", SaveValidationResult.FileNotFound);
            }

            SaveGameData data = JsonUtility.FromJson<SaveGameData>(json);

            // Validation
            string jsonForHash = SaveChecksumUtility.GetJsonForChecksumCalculation(data);
            string calculatedHash = SaveChecksumUtility.CalculateChecksum(jsonForHash);
            if (data.metadata.checksum != calculatedHash)
            {
                Debug.LogWarning("Checksum mismatch, save file may be altered. Allowing load for now.");
                // Return Error if strict, but let's allow with warning for now.
            }

            // Migration
            if (data.metadata.saveVersion != SaveVersion.Current)
            {
                foreach (var migration in _migrations)
                {
                    if (migration.CanMigrate(data.metadata.saveVersion, SaveVersion.Current))
                    {
                        migration.Migrate(data);
                        break;
                    }
                }
            }

            _registry.RestoreAll(data);
            Debug.Log($"Load slot {slot} successful. Recovered: {recovered}");
            return SaveOperationResult.Ok();
        }
        
        private bool ValidateJsonIntegrity(string json)
        {
            if (string.IsNullOrEmpty(json)) return false;
            try { JsonUtility.FromJson<SaveGameData>(json); return true; }
            catch { return false; }
        }

        private string GetSaveFilePath(int slot)
        {
            return Path.Combine(Application.persistentDataPath, $"save_slot_{slot}.json");
        }
    }
}
