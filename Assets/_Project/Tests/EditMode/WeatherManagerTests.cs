using NUnit.Framework;
using TheLastSprout.Core;
using TheLastSprout.Save;
using TheLastSprout.Weather;
using TheLastSprout.World;
using UnityEngine;

namespace TheLastSprout.Tests.EditMode
{
    public class WeatherManagerTests
    {
        [Test]
        public void StartWeather_PublishesEvent()
        {
            var registry = new ServiceRegistry();
            var eventBus = new EventBus();
            registry.Register(eventBus);

            var goTick = new GameObject();
            registry.Register(goTick.AddComponent<TickManager>());

            var saveManager = new SaveManager();
            saveManager.Initialize(eventBus);
            registry.Register(saveManager);

            var goWeather = new GameObject();
            var weatherManager = goWeather.AddComponent<WeatherManager>();
            weatherManager.Initialize(registry);

            bool eventFired = false;
            eventBus.Subscribe<WeatherStartedEvent>(e => eventFired = true);

            var def = ScriptableObject.CreateInstance<WeatherEventDefinition>();
            weatherManager.StartWeather(def, RegionId.Farm);

            Assert.IsTrue(eventFired);

            Object.DestroyImmediate(goTick);
            Object.DestroyImmediate(goWeather);
        }
    }
}
