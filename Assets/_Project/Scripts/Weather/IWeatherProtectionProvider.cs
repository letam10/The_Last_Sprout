using UnityEngine;

namespace TheLastSprout.Weather
{
    public interface IWeatherProtectionProvider
    {
        bool IsIndoors(Vector3 worldPosition);
        bool IsSheltered(Vector3 worldPosition);
        bool IsGreenhouse(Vector3 worldPosition);
    }
}
