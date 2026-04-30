using System.Collections.Generic;
using TheLastSprout.Core;

namespace TheLastSprout.Weather
{
    public class WeatherEffectProcessor
    {
        private readonly List<IWeatherAffectable> _affectables = new List<IWeatherAffectable>();
        private readonly Dictionary<WeatherType, WeatherEffectHandler> _handlers = new Dictionary<WeatherType, WeatherEffectHandler>();
        
        public void RegisterAffectable(IWeatherAffectable affectable)
        {
            if (!_affectables.Contains(affectable))
            {
                _affectables.Add(affectable);
            }
        }

        public void UnregisterAffectable(IWeatherAffectable affectable)
        {
            _affectables.Remove(affectable);
        }
        
        public void RegisterHandler(WeatherEffectHandler handler)
        {
            _handlers[handler.TargetType] = handler;
        }

        public void ProcessStart(WeatherContext context)
        {
            if (_handlers.TryGetValue(context.definition.weatherType, out var handler))
            {
                handler.OnWeatherStart(context);
            }

            for (int i = _affectables.Count - 1; i >= 0; i--)
            {
                if (_affectables[i].CanBeAffectedByWeather(context))
                {
                    _affectables[i].ApplyWeatherEffect(context);
                }
            }
        }

        public void ProcessTick(WeatherContext context)
        {
            if (_handlers.TryGetValue(context.definition.weatherType, out var handler))
            {
                handler.ProcessTick(context);
            }
        }

        public void ProcessEnd(WeatherContext context)
        {
            if (_handlers.TryGetValue(context.definition.weatherType, out var handler))
            {
                handler.OnWeatherEnd(context);
            }

            for (int i = _affectables.Count - 1; i >= 0; i--)
            {
                _affectables[i].ClearWeatherEffect(context);
            }
        }
    }
}
