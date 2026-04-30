using System;
using System.Collections.Generic;
using System.IO;
using TheLastSprout.Core;
using UnityEngine;

namespace TheLastSprout.Save
{
    public class SaveManager
    {
        private readonly List<ISaveable> _saveables = new List<ISaveable>();
        private EventBus _eventBus;
        private AtomicSaveWriter _atomicWriter;
        private BackupSaveHandler _backupHandler;
        
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
            _atomicWriter = new AtomicSaveWriter();
            _backupHandler = new BackupSaveHandler();
        }

        public void RegisterSaveable(ISaveable saveable)
        {
            if (!_saveables.Contains(saveable))
            {
                _saveables.Add(saveable);
            }
        }

        public void UnregisterSaveable(ISaveable saveable)
        {
            _saveables.Remove(saveable);
        }

        public void Save(int slot)
        {
            // In BossFight or Cutscene, saving might not be safe, but GameState check is externalized or done here.
            // Simplified check:
            // if (GameBootstrapper.Instance.Registry.Get<GameStateMachine>().CurrentState == GameState.BossFight) return;

            SaveGameData data = new SaveGameData();
            data.metadata.slotIndex = slot;
            data.metadata.saveVersion = SaveVersion.Current;
            data.metadata.timestampUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (var saveable in _saveables)
            {
                saveable.CaptureState(data);
            }

            string json = JsonUtility.ToJson(data, true);
            string path = GetSaveFilePath(slot);

            _backupHandler.RotateBackups(path);
            _atomicWriter.WriteSafely(path, json);
        }

        public void Load(int slot)
        {
            string path = GetSaveFilePath(slot);
            string json = null;

            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            else
            {
                json = _backupHandler.TryLoadNewestValidBackup(path);
            }

            if (!string.IsNullOrEmpty(json))
            {
                SaveGameData data = JsonUtility.FromJson<SaveGameData>(json);

                if (data.metadata.saveVersion != SaveVersion.Current)
                {
                    // Migration logic goes here
                }

                foreach (var saveable in _saveables)
                {
                    saveable.RestoreState(data);
                }
            }
        }

        private string GetSaveFilePath(int slot)
        {
            return Path.Combine(Application.persistentDataPath, $"save_slot_{slot}.json");
        }
    }
}
