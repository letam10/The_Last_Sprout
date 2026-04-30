using TheLastSprout.World;

namespace TheLastSprout.Weather
{
    public struct WeatherContext
    {
        public WeatherEventDefinition definition;
        public WeatherRuntimeState state;
        public RegionId region;
        public float deltaTime;
        public bool isTick;
        public IWeatherProtectionProvider protectionProvider;
    }
}
