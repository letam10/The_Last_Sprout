using System.IO;
using NUnit.Framework;
using TheLastSprout.Save;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class SaveCorruptRecoveryTests
    {
        [Test]
        public void CorruptSave_LoadsNewestValidBackup()
        {
            var eventBus = new TheLastSprout.Core.EventBus();
            var saveManager = new SaveManager();
            saveManager.Initialize(eventBus);

            // Path setup
            string primaryPath = Path.Combine(Application.persistentDataPath, "save_slot_99.json");
            string backupPath = primaryPath + ".bak";

            // Cleanup
            if (File.Exists(primaryPath)) File.Delete(primaryPath);
            if (File.Exists(backupPath)) File.Delete(backupPath);

            // 1. Write a valid backup manually
            var validData = new SaveGameData();
            validData.metadata.slotIndex = 99;
            validData.metadata.playtimeSeconds = 123;
            File.WriteAllText(backupPath, JsonUtility.ToJson(validData));

            // 2. Write a corrupt primary save
            File.WriteAllText(primaryPath, "{ THIS_IS_INVALID_JSON_]] }");

            // 3. Load
            var result = saveManager.Load(99);

            // Result should be OK because it recovered from backup.
            // Assuming SaveManager logs a warning and proceeds with backup.
            Assert.IsTrue(result.Success);

            // Clean up
            File.Delete(primaryPath);
            File.Delete(backupPath);
        }
    }
}
