using System.Collections.Generic;
using NUnit.Framework;
using TheLastSprout.Weather;
using TheLastSprout.World;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class WeatherSchedulerTests
    {
        [Test]
        public void Forecast_IsDeterministic_WithSameSeed()
        {
            var scheduler1 = new WeatherScheduler();
            scheduler1.Initialize(12345);

            var scheduler2 = new WeatherScheduler();
            scheduler2.Initialize(12345);

            var list = new List<WeatherEventDefinition>();
            var forecast1 = scheduler1.GenerateForecast(RegionId.Farm, 1, 5, list);
            var forecast2 = scheduler2.GenerateForecast(RegionId.Farm, 1, 5, list);

            Assert.AreEqual(forecast1[0].chance01, forecast2[0].chance01);
        }

        [Test]
        public void Forecast_IsDifferent_WithDifferentSeed()
        {
            var scheduler1 = new WeatherScheduler();
            scheduler1.Initialize(12345);

            var scheduler2 = new WeatherScheduler();
            scheduler2.Initialize(99999);

            var list = new List<WeatherEventDefinition>();
            var forecast1 = scheduler1.GenerateForecast(RegionId.Farm, 1, 5, list);
            var forecast2 = scheduler2.GenerateForecast(RegionId.Farm, 1, 5, list);

            Assert.AreNotEqual(forecast1[0].chance01, forecast2[0].chance01);
        }
    }
}
