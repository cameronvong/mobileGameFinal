using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Blackboards;
using Bunny.Entries;

namespace Bunny.Shared
{
    public interface IBunnyDatabase
    {
        IBunnyBlackboard GetBlackboardForEntry(string entry);
        void ApplyEntry(BunnyBaseEntry entry, BunnyTable table);
        bool TryGetEntry(string id, out BunnyBaseEntry candidate);
        bool TryGetRule(string id, IBunnyBlackboard context, out BunnyRuleEntry match);
        bool TryGetTable(string id, out BunnyTable table);
        bool TestEntry(BunnyBaseEntry entry, IBunnyBlackboard context);
        IBunnyBlackboard GetBlackboard(int scope, IBunnyBlackboard context);
        BunnyFactEntry GenerateFact(string key, IBunnyBlackboard context);
        BunnyEventEntry GenerateEvent(string key);
        BunnyRuleEntry GenerateRule(string key);
    }

    [CreateAssetMenu(fileName="BunnyDatabase", menuName="Bunny/Dialogue/Database", order = 0)]
    public class BunnyDatabase : ScriptableObject
    {
        public List<BunnyTable> tables;
        public Dictionary<BunnyEntryScope, IBunnyBlackboard> blackboards = new();
        public Dictionary<string, BunnyBaseEntry> entryLookup;
        public Dictionary<BunnyBaseEntry, BunnyTable> tableLookup;

        public static readonly object padlock = new object();
        private static BunnyDatabase _instance;

        public static BunnyDatabase Instance 
        {
            get
            {
                lock(padlock)
                {
                    if(_instance == null)
                    {
                        _instance = new BunnyDatabase();
                    }
                    return _instance;
                }
            }
        }

        public BunnyDatabase()
        {
            foreach(BunnyEntryScope scope in Enum.GetValues(typeof(BunnyEntryScope)))
            {
                blackboards.Add(scope, new BunnyBlackboard());
            }
        }

        public IBunnyBlackboard GetBlackboardForEntry(BunnyBaseEntry entry)
        {
            return blackboards[entry.scope];
        }

        public void ApplyEntry(BunnyBaseEntry entry, BunnyTable table)
        {
            table.AddEntry(entry);
        }

        public void Awake()
        {
            foreach(BunnyTable table in tables)
            {
                table.Initialize(this);
            }
        }
    }
}