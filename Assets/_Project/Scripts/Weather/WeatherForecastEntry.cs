using System;
using TheLastSprout.World;

namespace TheLastSprout.Weather
{
    [Serializable]
    public class WeatherForecastEntry
    {
        public int day;
        public RegionId region;
        public string weatherId;
        public WeatherType weatherType;
        public WeatherSeverity severity;
        public float chance01;
    }
}
