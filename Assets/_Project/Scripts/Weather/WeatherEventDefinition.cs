using System.Collections.Generic;
using TheLastSprout.Core.Data;
using TheLastSprout.World;
using UnityEngine;

namespace TheLastSprout.Weather
{
    [CreateAssetMenu(fileName = "NewWeatherEvent", menuName = "The Last Sprout/Weather/Weather Event Definition")]
    public class WeatherEventDefinition : BaseDefinition
    {
        public WeatherType weatherType;
        public WeatherSeverity severity;
        public int durationHours;
        public int forecastLeadDays;
        
        public bool canAffectCrops;
        public bool canAffectPlayer;
        public bool canAffectTown;
        public bool canSpawnEnemies;
        
        public bool affectsVisibility;
        public bool affectsMovement;
        public bool affectsStamina;
        
        public bool unlocksLore;
        public bool unlocksRegion;
        
        public float playerDamagePerTick;
        public float cropDamagePerTick;
        
        public float staminaDrainMultiplier = 1f;
        public float movementMultiplier = 1f;
        public float cropGrowthMultiplier = 1f;
        public float visibilityMultiplier = 1f;
        
        public string enemySpawnGroupId;
        public string visualEventId;
        public string audioEventId;
        public string loreEventId;
        
        public List<RegionId> allowedRegions = new List<RegionId>();
        public List<RegionId> blockedRegions = new List<RegionId>();
    }
}
