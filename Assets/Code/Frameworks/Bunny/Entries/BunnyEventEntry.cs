using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;

namespace Bunny.Entries
{  
    public delegate void EntryEvent();

    [Serializable]
    [CreateAssetMenu(fileName = "BunnyEventEntry", menuName = "Bunny/Entries/BunnyEventEntry", order = 0)]
    public class BunnyEventEntry : BunnyBaseEntry {

        public BunnyEvent entryEvent;

        public void Raise()
        {
            BunnyEventManager.Instance.Fire<string>(this.id, new BunnyMessage<string>(this.id, this));
        }

        public BunnyEventEntry()
        {
            entryEvent = BunnyEventManager.Instance.RegisterEvent(this.id, BunnyEventManager.Instance);
            // Debug.Log($"Entry event exists {entryEvent != null}");
        }

        public void Awake() {
            entryEvent = BunnyEventManager.Instance.RegisterEvent(this.id, BunnyEventManager.Instance);
        }

        public override BunnyEntryDescriptor GetDescriptor() {
            return BunnyEntryDescriptor.BunnyEventDescriptor;
        }
    }
}