using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny.Blackboards
{
    public interface IBunnyBlackboard
    {
        int Get(string id);
        void Set(string id, int value);
    }

    public class BunnyBlackboard : IBunnyBlackboard
    {
        private Dictionary<string, int> storage = new();
        private string Name;

        public bool TryGet(string id, out int value, int defaultValue = 0)
        {
            if(storage.ContainsKey(id))
            {
                value = storage[id];
                return true;
            }
            value = defaultValue;
            return false;
        }

        public int Get(string id)
        {
            if(!storage.ContainsKey(id))
                return 0;
            return storage[id];
        }

        public void Set(string id, int value)
        {
            storage[id] = value;
        }

        public void Clear()
        {
            storage.Clear();
        }
    }
}