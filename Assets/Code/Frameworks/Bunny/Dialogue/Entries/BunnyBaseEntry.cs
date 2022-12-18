using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyEntryType 
{
    FACT,
    EVENT,
    RULE
}

public class BunnyBaseEntry {
    [SerializeField]
    public int ID { get; set; }
    [SerializeField]
    public string Key { get; set; }

    public override bool Equals(object obj)
    {  
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        BunnyBaseEntry entry = obj as BunnyBaseEntry;
        if(entry == null)
            return false;
        
        if(entry.Key == Key) {
            return true;
        }
        return false;
    }

    // override object.GetHashCode
    public override int GetHashCode() => (Key).GetHashCode();
}