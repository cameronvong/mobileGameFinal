using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;
using Bunny.Shared;

namespace Bunny.Entries
{  
    public delegate void EntryEvent();

    public struct RuleEventArgs
    {
        public string eventId;
        public string ruleId;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "BunnyEventEntry", menuName = "Bunny/Entries/BunnyEventEntry", order = 0)]
    public class BunnyEventEntry : BunnyBaseEntry {

        public BunnyEvent entryEvent;
        public List<BunnyRuleEntry> onEnd = new();

        public void Raise()
        {
            onEnd.OrderBy(rule => rule.Weight).ToList();
            GetDescriptor().UpdateModifications(this);
            foreach(BunnyRuleEntry entry in onEnd)
            {
                bool isSatisfied = entry.GetDescriptor().Test(entry, BunnyDatabase.Instance);
                if(isSatisfied) {
                    BunnyEventManager.Instance.Fire<RuleEventArgs>(this.id, new BunnyMessage<RuleEventArgs>(new RuleEventArgs {
                        eventId = this.id,
                        ruleId = entry.id
                    }, this));
                    return;
                }
            }
            // BunnyEventManager.Instance.Fire<string>(this.id, new BunnyMessage<string>(this.id, this));
        }

        public void Awake()
        {
            entryEvent = BunnyEventManager.Instance.RegisterEvent(this.id, BunnyEventManager.Instance);
            onEnd = onEnd.OrderBy(rule => rule.Weight).ToList();
        }

        public BunnyEventEntry()
        {
            entryEvent = BunnyEventManager.Instance.RegisterEvent(this.id, BunnyEventManager.Instance);
            onEnd = onEnd.OrderBy(rule => rule.Weight).ToList();
            // Debug.Log($"Entry event exists {entryEvent != null}");
        }

        public void OnDestroy()
        {
            BunnyEventManager.Instance.Disconnect(this.id);
        }

        public override BunnyEntryDescriptor GetDescriptor() {
            return BunnyEntryDescriptor.BunnyEventDescriptor;
        }
    }
}