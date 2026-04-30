using TheLastSprout.Core;

namespace TheLastSprout.Weather
{
    public class SandstormWeatherHandler : WeatherEffectHandler
    {
        public override WeatherType TargetType => WeatherType.Sandstorm;

        public override void OnWeatherStart(WeatherContext context)
        {
            var eventBus = GameBootstrapper.Instance.Registry.Get<EventBus>();
            eventBus.Publish(new WeatherVisualRequestEvent { VisualEventId = context.definition.visualEventId });
            
            if (context.definition.unlocksRegion)
            {
                // Trigger Desert Dungeon unlock
                // eventBus.Publish(new UnlockRegionEvent { RegionId = RegionId.DesertDungeon });
            }
        }
    }
}
