using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We'll be using a sortedList. The keys are the rule entries 

public class BunnySortedRuleList : IEnumerable<BunnyRuleEntry>
{
    public List<BunnyRuleEntry> entries = new List<BunnyRuleEntry>();

    public IEnumerator<BunnyRuleEntry> GetEnumerator() {
        entries.Sort((a,b) => a.criterion.Length - b.criterion.Length);
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
        entries.Sort((a,b) => a.criterion.Length - b.criterion.Length);
    }

    public void Remove(BunnyRuleEntry entry)
    {
        entries.Remove(entry);
        entries.Sort((a,b) => a.criterion.Length - b.criterion.Length);
    }
}
