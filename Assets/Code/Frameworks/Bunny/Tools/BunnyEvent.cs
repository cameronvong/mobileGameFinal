using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Bad attempt from @Mohammedajao to visualize the events in Unity
*/

namespace Bunny.Tools 
{
    public interface IBunnyEvent {
        void OnEventRaised<T>(Action<BunnyMessage<T>> del);
        void Disconnect<T>(Action<BunnyMessage<T>> subscription);
        void Fire<T>(BunnyMessage<T> message);
    }

    [System.Serializable]
    public class BunnyEvent : IBunnyEvent
    {
        [SerializeField] private string eventName;
        public String EventName => eventName;
        public object Sender;
        private Dictionary<Type, List<Delegate>> _subscribers;

        public BunnyEvent(string name, object source)
        {
            eventName = name;
            Sender = source;
            _subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public void OnEventRaised<T>(Action<BunnyMessage<T>> callback)
        {
            var payloadType = typeof(T);
            if(!_subscribers.ContainsKey(payloadType))
            {
                _subscribers.Add(payloadType, new List<Delegate>());
            }
            if(!_subscribers[payloadType].Contains(callback))
                _subscribers[payloadType].Add(callback);
        }

        public void Fire<T>(BunnyMessage<T> message)
        {
            var payloadType = typeof(T);
            if(!_subscribers.ContainsKey(payloadType)) {
                Debug.LogWarning($"No subscribers found for BunnyEvent<{payloadType}> for EventName: {EventName}. Please check your code to see if subscribers are intentionally excluded.");
                return;
            }

            var delegates = _subscribers[typeof(T)];
            if (delegates == null || delegates.Count == 0) return;

            foreach(var handler in delegates.Select(item => item as Action<BunnyMessage<T>>))
            {
                handler?.Invoke(message);
            }
        }

        public void Disconnect<T>(Action<BunnyMessage<T>> subscription)
        {
            if(!_subscribers.ContainsKey(typeof(T)))
                return;
            var delegates = _subscribers[typeof(T)];
            if (delegates.Contains(subscription))
                delegates.Remove(subscription);
            if (delegates.Count == 0)
                _subscribers.Remove(typeof(T));
        }

        public void Disable()
        {
            _subscribers.Clear();
        }
    }
}
