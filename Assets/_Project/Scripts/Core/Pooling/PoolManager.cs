using System.Collections.Generic;
using UnityEngine;

namespace TheLastSprout.Core.Pooling
{
    public class PoolManager : MonoBehaviour
    {
        private readonly Dictionary<string, ObjectPool> _pools = new Dictionary<string, ObjectPool>();

        public void RegisterPool(string key, GameObject prefab, int initialSize)
        {
            if (!_pools.ContainsKey(key))
            {
                GameObject poolParent = new GameObject($"Pool_{key}");
                poolParent.transform.SetParent(transform);
                _pools[key] = new ObjectPool(prefab, poolParent.transform, initialSize);
            }
        }

        public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
        {
            if (_pools.TryGetValue(key, out var pool))
            {
                return pool.Get(position, rotation);
            }
            Debug.LogWarning($"Pool with key {key} not found!");
            return null;
        }

        public void Despawn(string key, GameObject instance)
        {
            if (_pools.TryGetValue(key, out var pool))
            {
                pool.Release(instance);
            }
            else
            {
                Destroy(instance);
            }
        }
    }
}
