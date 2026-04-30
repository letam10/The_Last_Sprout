namespace TheLastSprout.Weather
{
    public abstract class WeatherEffectHandler
    {
        public abstract WeatherType TargetType { get; }
        
        public virtual void OnWeatherStart(WeatherContext context) { }
        public virtual void ProcessTick(WeatherContext context) { }
        public virtual void OnWeatherEnd(WeatherContext context) { }
    }
}
