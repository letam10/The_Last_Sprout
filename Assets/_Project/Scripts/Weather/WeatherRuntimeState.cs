using System;
using System.Collections.Generic;
using TheLastSprout.World;

namespace TheLastSprout.Weather
{
    [Serializable]
    public class WeatherRuntimeState
    {
        public string currentWeatherId;
        public WeatherType currentWeatherType;
        public WeatherSeverity currentSeverity;
        public int remainingHours;
        public RegionId currentRegion;
        public List<WeatherForecastEntry> forecast = new List<WeatherForecastEntry>();
        public int randomSeed;
        public bool isActive;
    }
}
