using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Entries;
using Bunny.Blackboards;

namespace Bunny.Shared
{
    [Serializable]
    [CreateAssetMenu(fileName="BunnyTable", menuName="Bunny/Dialogue/Table", order = 0)]
    public class BunnyTable : ScriptableObject
    {
        public string Name;
        [SerializeReference] public List<BunnyFactEntry> facts;
        [SerializeReference] public List<BunnyEventEntry> events;
        [SerializeReference] public List<BunnyRuleEntry> rules;
        public Dictionary<string, string> EntryToIdMap;

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
            foreach(BunnyBaseEntry entry in facts)
            {
                database.GetBlackboardForEntry(entry).Set(entry.id, 0);
                EntryToIdMap.Add(entry.key, entry.id);
                database.entryLookup.Add(entry.id, entry);
                database.tableLookup.Add(entry, this);
            }
            foreach(BunnyBaseEntry entry in rules)
            {
                database.GetBlackboardForEntry(entry).Set(entry.id, 0);
                EntryToIdMap.Add(entry.key, entry.id);
                database.entryLookup.Add(entry.id, entry);
                database.tableLookup.Add(entry, this);
            }
            foreach(BunnyBaseEntry entry in events)
            {
                database.GetBlackboardForEntry(entry).Set(entry.id, 0);
                EntryToIdMap.Add(entry.key, entry.id);
                database.entryLookup.Add(entry.id, entry);
                database.tableLookup.Add(entry, this);
            }
        }
    }
}