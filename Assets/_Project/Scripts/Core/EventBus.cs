using System;
using System.Collections.Generic;

namespace TheLastSprout.Core
{
    public class EventBus
    {
        private readonly Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var existingHandlers))
            {
                _subscribers[type] = Delegate.Combine(existingHandlers, handler);
            }
            else
            {
                _subscribers[type] = handler;
            }
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var existingHandlers))
            {
                var currentHandlers = Delegate.Remove(existingHandlers, handler);
                if (currentHandlers == null)
                {
                    _subscribers.Remove(type);
                }
                else
                {
                    _subscribers[type] = currentHandlers;
                }
            }
        }

        public void Publish<T>(T eventData)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var handlers))
            {
                var action = handlers as Action<T>;
                action?.Invoke(eventData);
            }
        }

        public void Clear()
        {
            _subscribers.Clear();
        }
    }
}
