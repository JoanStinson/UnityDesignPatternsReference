using UnityEngine.Events;
using System.Collections.Generic;

namespace JGM.Patterns.EventBus
{
    public class RaceEventBus
    {
        protected RaceEventBus() { }

        private static readonly IDictionary<RaceEventType, UnityEvent> _events = new Dictionary<RaceEventType, UnityEvent>();

        public static void Subscribe(RaceEventType eventType, UnityAction listener)
        {
            if (_events.TryGetValue(eventType, out var @event))
            {
                @event?.AddListener(listener);
            }
            else
            {
                @event = new UnityEvent();
                @event?.AddListener(listener);
                _events.Add(eventType, @event);
            }
        }

        public static void Unsubscribe(RaceEventType type, UnityAction listener)
        {
            if (_events.TryGetValue(type, out var @event))
            {
                @event?.RemoveListener(listener);
            }
        }

        public static void Publish(RaceEventType type)
        {
            if (_events.TryGetValue(type, out var @event))
            {
                @event?.Invoke();
            }
        }
    }
}