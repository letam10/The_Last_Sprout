using System.Collections.Generic;
using TheLastSprout.World;

namespace TheLastSprout.Weather
{
    public class WeatherScheduler
    {
        private System.Random _random;

        public void Initialize(int seed)
        {
            _random = new System.Random(seed);
        }

        public List<WeatherForecastEntry> GenerateForecast(RegionId region, int startDay, int days, List<WeatherEventDefinition> availableWeathers)
        {
            var forecast = new List<WeatherForecastEntry>();

            for (int i = 0; i < days; i++)
            {
                var entry = new WeatherForecastEntry
                {
                    day = startDay + i,
                    region = region,
                    chance01 = (float)_random.NextDouble()
                };

                // Simple deterministic generation logic based on random roll
                WeatherEventDefinition picked = null;
                if (availableWeathers != null && availableWeathers.Count > 0)
                {
                    // Random pick for placeholder
                    picked = availableWeathers[_random.Next(availableWeathers.Count)];
                    
                    if (picked.blockedRegions.Contains(region) || (picked.allowedRegions.Count > 0 && !picked.allowedRegions.Contains(region)))
                    {
                        picked = null;
                    }
                }

                if (picked != null && entry.chance01 > 0.5f) // 50% chance of no weather event
                {
                    entry.weatherId = picked.StableId;
                    entry.weatherType = picked.weatherType;
                    entry.severity = picked.severity;
                }
                else
                {
                    entry.weatherId = "none";
                    entry.weatherType = WeatherType.None;
                    entry.severity = WeatherSeverity.Mild;
                }

                forecast.Add(entry);
            }

            return forecast;
        }
    }
}
