namespace TheLastSprout.Weather
{
    public interface IWeatherAffectable
    {
        bool CanBeAffectedByWeather(WeatherContext context);
        void ApplyWeatherEffect(WeatherContext context);
        void ClearWeatherEffect(WeatherContext context);
    }
}
