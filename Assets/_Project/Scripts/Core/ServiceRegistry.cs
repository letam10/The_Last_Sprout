using System;
using System.Collections.Generic;

namespace TheLastSprout.Core
{
    public class ServiceRegistry
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                _services[type] = service;
            }
            else
            {
                // Overwrite or log warning. For now, overwrite.
                _services[type] = service;
            }
        }

        public T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return service as T;
            }
            throw new Exception($"Service of type {typeof(T)} not found in ServiceRegistry.");
        }

        public bool TryGet<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = obj as T;
                return true;
            }
            service = null;
            return false;
        }

        public void Clear()
        {
            _services.Clear();
        }
    }
}
