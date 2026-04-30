using System.IO;
using TheLastSprout.Weather;
using UnityEditor;
using UnityEngine;

namespace TheLastSprout.Editor
{
    public class WeatherDefinitionCreatorEditor
    {
        private const string DataPath = "Assets/_Project/Data/Weather/";

        [MenuItem("Tools/The Last Sprout/Create Sample Weather Definitions")]
        public static void CreateSampleWeathers()
        {
            if (!AssetDatabase.IsValidFolder("Assets/_Project/Data"))
                AssetDatabase.CreateFolder("Assets/_Project", "Data");
            if (!AssetDatabase.IsValidFolder("Assets/_Project/Data/Weather"))
                AssetDatabase.CreateFolder("Assets/_Project/Data", "Weather");

            CreateWeatherAsset("Weather_AcidRain", WeatherType.AcidRain, WeatherSeverity.Medium, 12);
            CreateWeatherAsset("Weather_Hailstorm", WeatherType.Hailstorm, WeatherSeverity.Medium, 6);
            CreateWeatherAsset("Weather_Heatwave50", WeatherType.Heatwave50, WeatherSeverity.Severe, 24);
            CreateWeatherAsset("Weather_Blizzard", WeatherType.Blizzard, WeatherSeverity.Severe, 48);
            CreateWeatherAsset("Weather_Sandstorm", WeatherType.Sandstorm, WeatherSeverity.Medium, 18);
            CreateWeatherAsset("Weather_RedTide", WeatherType.RedTide, WeatherSeverity.Severe, 72);
            CreateWeatherAsset("Weather_StarRain", WeatherType.StarRain, WeatherSeverity.Mild, 8);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Sample Weather Definitions created successfully.");
        }

        private static void CreateWeatherAsset(string name, WeatherType type, WeatherSeverity severity, int duration)
        {
            string fullPath = $"{DataPath}{name}.asset";
            if (File.Exists(fullPath))
            {
                Debug.LogWarning($"Asset {name} already exists. Skipping.");
                return;
            }

            var asset = ScriptableObject.CreateInstance<WeatherEventDefinition>();
            asset.weatherType = type;
            asset.severity = severity;
            asset.durationHours = duration;

            // Simple sample data
            if (type == WeatherType.AcidRain)
            {
                asset.canAffectPlayer = true;
                asset.canAffectCrops = true;
                asset.playerDamagePerTick = 1f;
                asset.cropDamagePerTick = 5f;
            }
            else if (type == WeatherType.StarRain)
            {
                asset.unlocksLore = true;
            }

            AssetDatabase.CreateAsset(asset, fullPath);
        }
    }
}
