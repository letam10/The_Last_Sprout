using System;
using System.Collections.Generic;

namespace TheLastSprout.Save
{
    // Placeholders for specific module save data
    [Serializable] public class PlayerSaveData { public float health; public float stamina; }
    [Serializable] public class InventorySaveData { public List<string> itemIds; }
    [Serializable] public class FarmSaveData { public int totalHarvests; }
    [Serializable] public class CropSaveData { public string cropId; public int stage; }
    [Serializable] public class WeatherSaveData { /* migrated to specific class if needed, or keep generic */ }
    [Serializable] public class NPCSaveData { public string npcId; public int relationship; }
    [Serializable] public class QuestSaveData { public string questId; public bool isComplete; }
    [Serializable] public class RelationshipSaveData { public string npcId; public int hearts; }
    [Serializable] public class RegionSaveData { public string regionId; public bool isUnlocked; }
    [Serializable] public class TownRestorationSaveData { public int townLevel; }
    [Serializable] public class AutomationSpiritSaveData { public string spiritId; public float energy; }
    [Serializable] public class MachineSaveData { public string machineId; public bool isProcessing; }
}
