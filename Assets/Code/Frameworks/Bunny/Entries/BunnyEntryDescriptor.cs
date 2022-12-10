using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Shared;

namespace Bunny.Entries
{
    public enum BunnyEntryType
    {
        Fact,
        Event,
        Rule
    }

    public class BunnyEntryDescriptor
    {
        public BunnyEntryType type;

        public static BunnyEntryDescriptor BunnyFactDescriptor = new BunnyEntryDescriptor() { type = BunnyEntryType.Fact };
        public static BunnyEntryDescriptor BunnyEventDescriptor = new BunnyEntryDescriptor() { type = BunnyEntryType.Event };
        public static BunnyEntryDescriptor BunnyRuleDescriptor = new BunnyEntryDescriptor() { type = BunnyEntryType.Rule };

        public bool Test(BunnyBaseEntry entry, BunnyDatabase db)
        {
            for(int i = 0; i < entry.criteria.Length; i++)
            {
                bool isSatisfied = entry.criteria[i].Test(db);
                if(!isSatisfied) return false;
            }
            return true;
        }

        public void UpdateModifications(BunnyBaseEntry entry)
        {
            for(int i = 0; i < entry.modifications.Length; i++) {
                entry.modifications[i].Execute(BunnyDatabase.Instance);
            }
        }

        public void AddToTable(BunnyBaseEntry entry, BunnyTable table)
        {
            switch(type) {
                case BunnyEntryType.Fact:
                    table.facts.Add((BunnyFactEntry) entry);
                    break;
                case BunnyEntryType.Event:
                    table.events.Add((BunnyEventEntry) entry);
                    break;
                case BunnyEntryType.Rule:
                    table.rules.Add((BunnyRuleEntry) entry);
                    break;
                default:
                    break;
            }
        }

        public void RemoveFromTable(BunnyBaseEntry entry, BunnyTable table)
        {
            switch(type) {
                case BunnyEntryType.Fact:
                    table.facts.Add((BunnyFactEntry) entry);
                    break;
                case BunnyEntryType.Event:
                    table.events.Add((BunnyEventEntry) entry);
                    break;
                case BunnyEntryType.Rule:
                    table.rules.Add((BunnyRuleEntry) entry);
                    break;
                default:
                    break;
            }
        }
    }
}