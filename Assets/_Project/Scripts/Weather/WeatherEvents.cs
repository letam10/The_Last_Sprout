namespace TheLastSprout.Weather
{
    // Stub Events
    public struct WeatherStartedEvent { public WeatherEventDefinition Definition; }
    public struct WeatherEndedEvent { public WeatherEventDefinition Definition; }
    public struct WeatherTickEvent { public WeatherEventDefinition Definition; public float DeltaTime; }
    public struct WeatherForecastChangedEvent { }

    public struct PlayerWeatherEffectEvent { public float Damage; public float StaminaMultiplier; }
    public struct CropWeatherEffectEvent { public float Damage; }
    public struct TownWeatherDamageEvent { }
    public struct EnemyWeatherSpawnRequestEvent { public string SpawnGroupId; }
    public struct WeatherVisualRequestEvent { public string VisualEventId; }
    public struct WeatherAudioRequestEvent { public string AudioEventId; }
    public struct SpiritRechargeWeatherEvent { }
    public struct LoreWeatherEvent { public string LoreEventId; }
}
