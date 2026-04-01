using System.Collections.Generic;
using System.Linq;

namespace PatternObserver.GameEvents
{
    public class EventManager
    {
        private Dictionary<GameEventType, List<IObserver>> _subscribers;

        public EventManager()
        {
            _subscribers = new Dictionary<GameEventType, List<IObserver>>();
        }

        public void Subscribe(GameEventType eventType, IObserver observer)
        {
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers.Add(eventType, new List<IObserver>());
            }

            _subscribers[eventType].Add(observer);
        }

        public void Unsubscribe(GameEventType eventType, IObserver observer)
        {
            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType].Remove(observer);
            }
        }

        public void UnsubscribeAll()
        {
            _subscribers.Clear();
        }

        public void Notify(GameEventData eventData)
        {
            if (!_subscribers.TryGetValue(eventData.Type, out var subscribers))
                return;

            var subscribersCopy = subscribers.ToList();

            foreach (var subscriber in subscribersCopy)
            {
                subscriber.OnNotify(eventData);
            }

            for (int i = subscribers.Count - 1; i >= 0; i--)
            {
                if (subscribers[i] is ICompletableObserver completableObserver && completableObserver.IsCompleted)
                {
                    subscribers.RemoveAt(i);
                }
            }
        }
    }
}
