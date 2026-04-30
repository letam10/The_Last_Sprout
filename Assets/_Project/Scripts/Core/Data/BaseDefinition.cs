using UnityEngine;

namespace TheLastSprout.Core.Data
{
    public abstract class BaseDefinition : ScriptableObject
    {
        [SerializeField] private string stableId;
        [SerializeField] private string displayName;

        public string StableId => stableId;
        public string DisplayName => displayName;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(stableId))
            {
                Debug.LogWarning($"[BaseDefinition] Stable ID is empty on {name}. Generating a default one.");
                stableId = System.Guid.NewGuid().ToString();
            }
        }
    }
}
