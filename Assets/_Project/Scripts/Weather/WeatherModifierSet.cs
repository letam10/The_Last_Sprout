using System;

namespace TheLastSprout.Weather
{
    public struct WeatherModifierSet
    {
        public float staminaMultiplier;
        public float movementMultiplier;
        public float cropGrowthMultiplier;
        public float visibilityMultiplier;
        public float playerDamagePerTick;
        public float cropDamagePerTick;
        
        public static WeatherModifierSet Default => new WeatherModifierSet
        {
            staminaMultiplier = 1f,
            movementMultiplier = 1f,
            cropGrowthMultiplier = 1f,
            visibilityMultiplier = 1f,
            playerDamagePerTick = 0f,
            cropDamagePerTick = 0f
        };
    }
}
