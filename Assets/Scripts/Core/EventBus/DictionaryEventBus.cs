

using System;
using System.Collections.Generic;

namespace Core.EventBus
{
    public class DictionaryEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> handlersByEventType = new();

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            Type eventType = typeof(TEvent);

            if (!handlersByEventType.TryGetValue(eventType, out List<Delegate> handlers))
            {
                handlers = new List<Delegate>();
                handlersByEventType.Add(eventType, handlers);
            }

            handlers.Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            Type eventType = typeof(TEvent);

            if (!handlersByEventType.TryGetValue(eventType, out List<Delegate> handlers))
                return;

            for (int handlerIndex = handlers.Count - 1; handlerIndex >= 0; handlerIndex--)
            {
                if (ReferenceEquals(handlers[handlerIndex], handler))
                    handlers.RemoveAt(handlerIndex);
            }

            if (handlers.Count == 0)
                handlersByEventType.Remove(eventType);
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            Type eventType = typeof(TEvent);

            if (!handlersByEventType.TryGetValue(eventType, out List<Delegate> handlers))
                return;

            for (int handlerIndex = handlers.Count - 1; handlerIndex >= 0; handlerIndex--)
            {
                if (handlers[handlerIndex] is Action<TEvent> typedHandler)
                    typedHandler(eventData);
            }
        }
    }
}
