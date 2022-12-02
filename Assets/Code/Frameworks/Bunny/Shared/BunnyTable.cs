using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Entries;

namespace Bunny.Shared
{
    [Serializable]
    public class BunnyTable : ScriptableObject
    {
        public string Name;
        [SerializeReference] public List<BunnyFactEntry> facts;
        [SerializeReference] public List<BunnyEventEntry> events;
        [SerializeReference] public List<BunnyRuleEntry> rules;

        public virtual void RemoveEntry(BunnyBaseEntry entry)
        {
            entry.GetDescriptor().RemoveFromTable(entry, this);
        }

        public virtual void AddEntry(BunnyBaseEntry entry)
        {
            entry.GetDescriptor().AddToTable(entry, this);
        }

        public virtual void Initialize(BunnyDatabase database)
        {
        }
    }
}