using System;

namespace TheLastSprout.Settings
{
    [Serializable]
    public class SettingsData
    {
        public float masterVolume = 1.0f;
        public float musicVolume = 1.0f;
        public float sfxVolume = 1.0f;
        
        public int resolutionWidth = 1920;
        public int resolutionHeight = 1080;
        public bool fullscreen = true;
        
        public float textScale = 1.0f;
        public bool cameraShake = true;
        
        public string language = "en";
        
        // inputBinding placeholders
    }
}
