using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;
using Bunny.Structures;

namespace Bunny.Entries
{    
    [CreateAssetMenu(fileName = "BunnyRuleEntry", menuName = "Bunny/Entries/BunnyRuleEntry", order = 0)]
    public class BunnyRuleEntry : BunnyBaseEntry {
        [SerializeField] public bool isCancellable;
        [SerializeField] private int padding;
        [SerializeField] private float delay;

        [SerializeField]
        public BunnyEventEntry triggeredBy;

        public BunnyEventEntry triggers;

        public int Weight => criteria.Length + padding;

        public float Delay => delay;

        public SortedRuleList onEnd = new SortedRuleList();

        Action<BunnyMessage<string>> callback;

        public BunnyRuleEntry() {
            callback = this.Execute;
        }


        public override BunnyEntryDescriptor GetDescriptor() {
            return BunnyEntryDescriptor.BunnyRuleDescriptor;
        }

        public void Execute(BunnyMessage<string> message)
        {
            Debug.Log($"Rule - {this.id} executed");
            if(triggers != null)
                triggers.Raise();
        }

        public void Awake() {
            BunnyEventManager.Instance.OnEventRaised<string>(triggeredBy.id, callback);
        }

        void OnDisable()
        {
            // triggeredBy.entryEvent -= Execute;
        }
    }
}