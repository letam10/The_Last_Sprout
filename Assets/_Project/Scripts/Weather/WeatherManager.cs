using System.Collections.Generic;
using TheLastSprout.Core;
using TheLastSprout.Save;
using TheLastSprout.World;
using UnityEngine;

namespace TheLastSprout.Weather
{
    public class WeatherManager : MonoBehaviour, ISaveable, ITickable
    {
        private ServiceRegistry _registry;
        private EventBus _eventBus;
        
        private WeatherRuntimeState _state;
        private WeatherScheduler _scheduler;
        private WeatherEffectProcessor _processor;
        
        private WeatherEventDefinition _currentDefinition;
        private IWeatherProtectionProvider _protectionProvider; // Mock or real

        public string SaveId => "WeatherManager";

        public void Initialize(ServiceRegistry registry)
        {
            _registry = registry;
            _eventBus = _registry.Get<EventBus>();

            _state = new WeatherRuntimeState
            {
                randomSeed = System.Environment.TickCount,
                isActive = false
            };

            _scheduler = new WeatherScheduler();
            _scheduler.Initialize(_state.randomSeed);

            _processor = new WeatherEffectProcessor();
            _processor.RegisterHandler(new AcidRainWeatherHandler());
            _processor.RegisterHandler(new HeatwaveWeatherHandler());
            _processor.RegisterHandler(new SandstormWeatherHandler());
            _processor.RegisterHandler(new StarRainWeatherHandler());

            _registry.Get<TickManager>().Register(this, TickGroup.Weather);
            _registry.Get<SaveManager>().RegisterSaveable(this);
        }

        private void OnDestroy()
        {
            if (_registry != null)
            {
                _registry.Get<TickManager>()?.Unregister(this, TickGroup.Weather);
                _registry.Get<SaveManager>()?.UnregisterSaveable(this);
            }
        }

        public void SetProtectionProvider(IWeatherProtectionProvider provider)
        {
            _protectionProvider = provider;
        }

        public void StartWeather(WeatherEventDefinition definition, RegionId region)
        {
            if (_state.isActive)
            {
                EndCurrentWeather();
            }

            _currentDefinition = definition;
            _state.isActive = true;
            _state.currentWeatherId = definition.StableId;
            _state.currentWeatherType = definition.weatherType;
            _state.currentSeverity = definition.severity;
            _state.remainingHours = definition.durationHours;
            _state.currentRegion = region;

            var context = CreateContext(0f, false);
            _processor.ProcessStart(context);

            _eventBus.Publish(new WeatherStartedEvent { Definition = _currentDefinition });
        }

        public void EndCurrentWeather()
        {
            if (!_state.isActive) return;

            var context = CreateContext(0f, false);
            _processor.ProcessEnd(context);

            var oldDef = _currentDefinition;
            
            _state.isActive = false;
            _state.currentWeatherId = null;
            _state.currentWeatherType = WeatherType.None;
            _currentDefinition = null;

            _eventBus.Publish(new WeatherEndedEvent { Definition = oldDef });
        }

        public void Tick(float deltaTime)
        {
            if (!_state.isActive || _currentDefinition == null) return;

            // Simplified: decrease remaining hours (normally tied to GameTimeManager)
            // Here we assume deltaTime correlates to game time for testing.
            
            var context = CreateContext(deltaTime, true);
            _processor.ProcessTick(context);
            
            _eventBus.Publish(new WeatherTickEvent { Definition = _currentDefinition, DeltaTime = deltaTime });

            // If time runs out, end weather.
            // Placeholder condition:
            // if (_state.remainingHours <= 0) EndCurrentWeather();
        }

        private WeatherContext CreateContext(float deltaTime, bool isTick)
        {
            return new WeatherContext
            {
                definition = _currentDefinition,
                state = _state,
                region = _state.currentRegion,
                deltaTime = deltaTime,
                isTick = isTick,
                protectionProvider = _protectionProvider
            };
        }

        public void RegisterAffectable(IWeatherAffectable affectable)
        {
            _processor?.RegisterAffectable(affectable);
        }

        public void UnregisterAffectable(IWeatherAffectable affectable)
        {
            _processor?.UnregisterAffectable(affectable);
        }

        public void GenerateForecast(RegionId region, int startDay, int days, List<WeatherEventDefinition> availableWeathers)
        {
            _state.forecast = _scheduler.GenerateForecast(region, startDay, days, availableWeathers);
            _eventBus.Publish(new WeatherForecastChangedEvent());
        }

        public void CaptureState(SaveGameData data)
        {
            string json = JsonUtility.ToJson(_state);
            data.weatherState.Add(new StringObjectPair { key = "state", jsonValue = json });
        }

        public void RestoreState(SaveGameData data)
        {
            var pair = data.weatherState.Find(p => p.key == "state");
            if (!string.IsNullOrEmpty(pair.jsonValue))
            {
                _state = JsonUtility.FromJson<WeatherRuntimeState>(pair.jsonValue);
                _scheduler.Initialize(_state.randomSeed);
                
                // If it was active, we need to re-load the Definition from a DataManager (not implemented here)
                // and resume it.
            }
        }
    }
}
