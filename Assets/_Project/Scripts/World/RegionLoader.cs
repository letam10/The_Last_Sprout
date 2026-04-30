using System;
using UnityEngine;

namespace TheLastSprout.World
{
    public class RegionLoader
    {
        public event Action<RegionId> OnRegionLoadStarted;
        public event Action<RegionId> OnRegionLoadCompleted;

        public void LoadRegionAsync(RegionId region)
        {
            OnRegionLoadStarted?.Invoke(region);
            
            // Placeholder: Load scene async, manage additive loading etc.
            Debug.Log($"Loading region: {region}");
            
            OnRegionLoadCompleted?.Invoke(region);
        }
    }
}
