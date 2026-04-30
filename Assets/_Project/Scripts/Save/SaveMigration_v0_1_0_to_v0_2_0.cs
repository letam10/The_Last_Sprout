using UnityEngine;

namespace TheLastSprout.Save
{
    public class SaveMigration_v0_1_0_to_v0_2_0 : ISaveMigration
    {
        public bool CanMigrate(string fromVersion, string toVersion)
        {
            return fromVersion == "0.1.0" && toVersion == "0.2.0";
        }

        public void Migrate(SaveGameData data)
        {
            Debug.Log("[SaveMigration] Migrating save data from v0.1.0 to v0.2.0");
            
            // Example: Move old list data to specific structures or just update version
            data.metadata.saveVersion = "0.2.0";
            
            // Any specific data transformation logic goes here
            if (data.metadata.playtimeSeconds < 0)
                data.metadata.playtimeSeconds = 0;
        }
    }
}
