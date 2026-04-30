using TheLastSprout.Core;

namespace TheLastSprout.Weather
{
    public class HeatwaveWeatherHandler : WeatherEffectHandler
    {
        public override WeatherType TargetType => WeatherType.Heatwave50;

        public override void OnWeatherStart(WeatherContext context)
        {
            var eventBus = GameBootstrapper.Instance.Registry.Get<EventBus>();
            eventBus.Publish(new WeatherVisualRequestEvent { VisualEventId = context.definition.visualEventId });
            eventBus.Publish(new WeatherAudioRequestEvent { AudioEventId = context.definition.audioEventId });
        }

        public override void ProcessTick(WeatherContext context)
        {
            var eventBus = GameBootstrapper.Instance.Registry.Get<EventBus>();

            if (context.definition.canAffectPlayer)
            {
                eventBus.Publish(new PlayerWeatherEffectEvent 
                { 
                    Damage = context.definition.playerDamagePerTick,
                    StaminaMultiplier = context.definition.staminaDrainMultiplier
                });
            }
            
            // Mirage event placeholder could be visually triggered here via eventbus
        }
    }
}
