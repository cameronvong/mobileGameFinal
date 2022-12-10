using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Entries;

namespace Bunny.Structures
{
    [Serializable]
    public class SortedRuleList : IEnumerable<BunnyRuleEntry>
    {
        public List<BunnyRuleEntry> entries = new List<BunnyRuleEntry>();

        public IEnumerator<BunnyRuleEntry> GetEnumerator() {
            foreach(var item in entries)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        
        public void Add(BunnyRuleEntry entry)
        {
            entries.Add(entry);
            entries.Sort((a,b) => a.Weight - b.Weight);
        }

        public void Remove(BunnyRuleEntry entry)
        {
            entries.Remove(entry);
            entries.Sort((a,b) => a.Weight - b.Weight);
        }
    }
}