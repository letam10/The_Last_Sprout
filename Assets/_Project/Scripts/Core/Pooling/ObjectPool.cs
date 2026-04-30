using System.Collections.Generic;
using UnityEngine;

namespace TheLastSprout.Core.Pooling
{
    public class ObjectPool
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly Stack<GameObject> _pool = new Stack<GameObject>();
        private readonly int _maxSize;

        public ObjectPool(GameObject prefab, Transform parent, int initialSize, int maxSize = 1000)
        {
            _prefab = prefab;
            _parent = parent;
            _maxSize = maxSize;
            Prewarm(initialSize);
        }

        private void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var instance = Object.Instantiate(_prefab, _parent);
                instance.SetActive(false);
                _pool.Push(instance);
            }
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject instance;
            if (_pool.Count > 0)
            {
                instance = _pool.Pop();
                instance.transform.position = position;
                instance.transform.rotation = rotation;
                instance.SetActive(true);
            }
            else
            {
                instance = Object.Instantiate(_prefab, position, rotation, _parent);
            }

            var poolable = instance.GetComponent<IPoolable>();
            poolable?.OnSpawned();

            return instance;
        }

        public void Release(GameObject instance)
        {
            var poolable = instance.GetComponent<IPoolable>();
            poolable?.OnDespawned();

            instance.SetActive(false);
            if (_pool.Count < _maxSize)
            {
                _pool.Push(instance);
            }
            else
            {
                Object.Destroy(instance);
            }
        }
    }
}
