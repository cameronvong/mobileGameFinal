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