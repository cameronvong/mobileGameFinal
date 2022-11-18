using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class EventRegistryException : Exception
{
    public EventRegistryException() : base() { }
    public EventRegistryException(string message) : base(message) { }
    public EventRegistryException(string message, Exception inner) : base(message, inner) { }
}

public class BunnyEventManager : MonoBehaviour
{

    private static GameObject _instance;
    public static readonly object padlock = new object();
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
                    _instance = new GameObject("BunnyEventManager");
                    _instance.AddComponent<BunnyEventManager>();
                }
                return _instance.GetComponent<BunnyEventManager>();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BunnyEvent RegisterEvent(string EventName, object source)
    {
        if(_events.ContainsKey(EventName)) {
            throw new EventRegistryException($"Event[{EventName}] already exists. Please choose a unique name.");
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