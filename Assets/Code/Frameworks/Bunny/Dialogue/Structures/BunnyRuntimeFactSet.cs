using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class BunnyRuntimeSetException : Exception
{
    public BunnyRuntimeSetException() {}
    public BunnyRuntimeSetException(string message) {}
}

public abstract class BunnyRuntimeFactSet<T> : ScriptableObject
{
    public Dictionary<BunnyFactEntry, T> items = new Dictionary<BunnyFactEntry, T>();

    public void Initialize()
    {
        items.Clear();
    }

    public T GetItemIndex(BunnyFactEntry entry)
    {
        if(!items.ContainsKey(entry))
            throw new BunnyRuntimeSetException("[RuntimeFactSet] Key for fact does not exist");
        return items[entry];
    }

    public void Add(BunnyFactEntry entry, T val)
    {
        if(!items.ContainsKey(entry))
        {
            items[entry] = val;
        }
    }

    public void Remove(BunnyFactEntry entry)
    {
        if(items.ContainsKey(entry))
        {
            items.Remove(entry);
        }
    }
}
