using System.Collections.Generic;
using UnityEngine;

namespace TheLastSprout.Spirits
{
    public class SpiritTaskScheduler
    {
        private readonly Dictionary<AutomationSpiritRole, List<ISpiritTaskProvider>> _providers = new Dictionary<AutomationSpiritRole, List<ISpiritTaskProvider>>();

        public void RegisterProvider(AutomationSpiritRole role, ISpiritTaskProvider provider)
        {
            if (!_providers.ContainsKey(role))
                _providers[role] = new List<ISpiritTaskProvider>();
            
            if (!_providers[role].Contains(provider))
                _providers[role].Add(provider);
        }

        public void UnregisterProvider(AutomationSpiritRole role, ISpiritTaskProvider provider)
        {
            if (_providers.ContainsKey(role))
                _providers[role].Remove(provider);
        }

        public SpiritTaskRequest? GetBestTask(AutomationSpiritRole role, Vector3 spiritPosition, int spiritLevel)
        {
            if (!_providers.TryGetValue(role, out var roleProviders)) return null;

            SpiritTaskRequest? bestRequest = null;
            float highestPriority = -1f;

            foreach (var provider in roleProviders)
            {
                var req = provider.GetNextTask(role, spiritPosition, spiritLevel);
                if (req.HasValue && req.Value.priority > highestPriority)
                {
                    highestPriority = req.Value.priority;
                    bestRequest = req;
                }
            }

            return bestRequest;
        }
    }
}
