using TheLastSprout.Core;

namespace TheLastSprout.Weather
{
    public class StarRainWeatherHandler : WeatherEffectHandler
    {
        public override WeatherType TargetType => WeatherType.StarRain;

        public override void OnWeatherStart(WeatherContext context)
        {
            var eventBus = GameBootstrapper.Instance.Registry.Get<EventBus>();
            eventBus.Publish(new WeatherVisualRequestEvent { VisualEventId = context.definition.visualEventId });
            
            eventBus.Publish(new SpiritRechargeWeatherEvent());

            if (context.definition.unlocksLore && !string.IsNullOrEmpty(context.definition.loreEventId))
            {
                eventBus.Publish(new LoreWeatherEvent { LoreEventId = context.definition.loreEventId });
            }
        }
    }
}
