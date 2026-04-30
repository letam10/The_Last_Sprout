using System.IO;
using UnityEngine;

namespace TheLastSprout.Settings
{
    public class SettingsManager
    {
        private SettingsData _currentSettings;

        public void Load()
        {
            string path = GetSettingsPath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _currentSettings = JsonUtility.FromJson<SettingsData>(json);
            }
            else
            {
                _currentSettings = new SettingsData();
            }
            ApplySettings();
        }

        public void Save()
        {
            string path = GetSettingsPath();
            string json = JsonUtility.ToJson(_currentSettings, true);
            File.WriteAllText(path, json);
        }

        private void ApplySettings()
        {
            // Placeholder: apply resolution, volumes, etc.
        }

        private string GetSettingsPath()
        {
            return Path.Combine(Application.persistentDataPath, "settings.json");
        }
    }
}
