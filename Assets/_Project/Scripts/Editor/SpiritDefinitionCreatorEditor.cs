using System.IO;
using TheLastSprout.Core.Data;
using TheLastSprout.Spirits;
using UnityEditor;
using UnityEngine;

namespace TheLastSprout.Editor
{
    public class SpiritDefinitionCreatorEditor
    {
        private const string DataPath = "Assets/_Project/Data/Spirits/";

        [MenuItem("Tools/The Last Sprout/Create Sample Spirits")]
        public static void CreateSampleSpirits()
        {
            if (!AssetDatabase.IsValidFolder("Assets/_Project/Data"))
                AssetDatabase.CreateFolder("Assets/_Project", "Data");
            if (!AssetDatabase.IsValidFolder("Assets/_Project/Data/Spirits"))
                AssetDatabase.CreateFolder("Assets/_Project/Data", "Spirits");

            CreateSpirit("Spirit_MamMua", AutomationSpiritRole.Watering, EnergySourceType.Moonlight, 100f);
            CreateSpirit("Spirit_LuaNho", AutomationSpiritRole.Harvesting, EnergySourceType.Sunlight, 80f);
            CreateSpirit("Spirit_CoMay", AutomationSpiritRole.Transport, EnergySourceType.RareMaterial, 120f);
            CreateSpirit("Spirit_ThanHong", AutomationSpiritRole.MachineOperator, EnergySourceType.Sunlight, 150f);
            CreateSpirit("Spirit_DomDomThep", AutomationSpiritRole.CombatAssist, EnergySourceType.StarRain, 200f);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Sample Spirit Definitions created successfully.");
        }

        private static void CreateSpirit(string name, AutomationSpiritRole role, EnergySourceType source, float maxEnergy)
        {
            string fullPath = $"{DataPath}{name}.asset";
            if (File.Exists(fullPath))
            {
                Debug.LogWarning($"Asset {name} already exists. Skipping.");
                return;
            }

            var asset = ScriptableObject.CreateInstance<AutomationSpiritDefinition>();
            asset.role = role;
            asset.preferredEnergySource = source;
            asset.maxEnergy = maxEnergy;
            asset.moveSpeed = 3f;
            asset.workSpeedMultiplier = 1f;

            AssetDatabase.CreateAsset(asset, fullPath);
        }
    }
}
