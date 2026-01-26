using System;
using System.Collections.Generic;
using System.Linq;

namespace TestAvalonia2
{
    public interface IEventAggregator
    {
        void Subscribe<T>(Action<T> handler);
        void Unsubscribe<T>(Action<T> handler);
        void Publish<T>(T message);
    }
    internal class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Delegate>();

            _handlers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type))
                _handlers[type].Remove(handler);
        }

        public void Publish<T>(T message)
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type))
            {
                foreach (var handler in _handlers[type].ToList())
                {
                    ((Action<T>)handler)?.Invoke(message);
                }
            }
        }
    }
}