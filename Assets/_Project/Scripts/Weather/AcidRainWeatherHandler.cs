using System;
using TheLastSprout.Core;

namespace TheLastSprout.Weather
{
    public class AcidRainWeatherHandler : WeatherEffectHandler
    {
        public override WeatherType TargetType => WeatherType.AcidRain;

        public override void OnWeatherStart(WeatherContext context)
        {
            var eventBus = GameBootstrapper.Instance.Registry.Get<EventBus>();
            eventBus.Publish(new WeatherVisualRequestEvent { VisualEventId = context.definition.visualEventId });
            eventBus.Publish(new WeatherAudioRequestEvent { AudioEventId = context.definition.audioEventId });
        }

        public override void ProcessTick(WeatherContext context)
        {
            var eventBus = GameBootstrapper.Instance.Registry.Get<EventBus>();
            
            // Example of raising events for systems to catch instead of iterating affectables directly here
            if (context.definition.canAffectPlayer)
            {
                eventBus.Publish(new PlayerWeatherEffectEvent 
                { 
                    Damage = context.definition.playerDamagePerTick,
                    StaminaMultiplier = context.definition.staminaDrainMultiplier
                });
            }

            if (context.definition.canAffectCrops)
            {
                eventBus.Publish(new CropWeatherEffectEvent { Damage = context.definition.cropDamagePerTick });
            }

            if (context.definition.canSpawnEnemies && !string.IsNullOrEmpty(context.definition.enemySpawnGroupId))
            {
                eventBus.Publish(new EnemyWeatherSpawnRequestEvent { SpawnGroupId = context.definition.enemySpawnGroupId });
            }
        }
    }
}
