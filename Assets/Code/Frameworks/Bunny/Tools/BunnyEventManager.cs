using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Bunny.Tools
{
    
    [Serializable]
    public class EventRegistryException : Exception
    {
        public EventRegistryException() : base() { }
        public EventRegistryException(string message) : base(message) { }
        public EventRegistryException(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]
    public class BunnyEventManager
    {

        public static readonly object padlock = new object();
        private static BunnyEventManager _instance;
        [ReadOnly] public List<string> EventLibrary;
        private Dictionary<string, BunnyEvent> _events;


        private BunnyEventManager() {
            EventLibrary = new List<string>();
            _events = new Dictionary<string, BunnyEvent>();
        }

        // Establish singleton design pattern
        public static BunnyEventManager Instance 
        {
            get
            {
                lock(padlock)
                {
                    if(_instance == null)
                    {
                        _instance = new BunnyEventManager();
                    }
                    return _instance;
                }
            }
        }

        public bool TryGetEvent(string EventName, out BunnyEvent dataEvent)
        {
            return _events.TryGetValue(EventName, out dataEvent);
        }

        public BunnyEvent RegisterEvent(string EventName, object source)
        {
            if(_events.ContainsKey(EventName)) {
                return _events[EventName];
                // throw new EventRegistryException($"Event[{EventName}] already exists. Please choose a unique name.");
            }
            BunnyEvent nEvent = new BunnyEvent(EventName, source);
            _events[EventName] = nEvent;
            EventLibrary.Add(EventName);
            return nEvent;
        }

        public BunnyEvent FetchEvent(string EventName)
        {
            if(!_events.ContainsKey(EventName))
                throw new EventRegistryException($"Event[{EventName}] does not exist.");
            return _events[EventName];
        }

        public void Fire<T>(string EventName, BunnyMessage<T> message)
        {
            if(!_events.ContainsKey(EventName)) {
                Debug.LogWarning($"A request to raise Event[{EventName}] was made but the event doesn't exist.");
                return;
            }
            _events[EventName].Fire(message);
        }

        public void OnEventRaised<T>(string EventName, Action<BunnyMessage<T>> callback)
        {
            if(!_events.ContainsKey(EventName)) {
                Debug.LogWarning($"A subscription for Event[{EventName}] was attempted but the event doesn't exist.");
                return;
            }
            _events[EventName].OnEventRaised<T>(callback);
        }

        public void Disconnect(string EventName)
        {
            if(!_events.ContainsKey(EventName))
                return;
            _events[EventName].Disable();
            _events.Remove(EventName);
            EventLibrary.Remove(EventName);
        }

        void OnDisable()
        {
            foreach(var eventName in EventLibrary)
            {
                _events[eventName].Disable();
            }
            _events.Clear();
            EventLibrary.Clear();
        }
    }
}