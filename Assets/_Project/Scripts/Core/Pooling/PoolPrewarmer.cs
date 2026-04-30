using UnityEngine;

namespace TheLastSprout.Core.Pooling
{
    public class PoolPrewarmer : MonoBehaviour
    {
        [System.Serializable]
        public struct PoolConfig
        {
            public string key;
            public GameObject prefab;
            public int initialSize;
        }

        [SerializeField] private PoolConfig[] poolsToPrewarm;

        public void Prewarm(PoolManager poolManager)
        {
            foreach (var config in poolsToPrewarm)
            {
                poolManager.RegisterPool(config.key, config.prefab, config.initialSize);
            }
        }
    }
}
