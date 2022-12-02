using System;
using Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Blackboards;

namespace Bunny.Entries
{
    [Serializable]
    public abstract class BunnyBaseEntry : ScriptableObject
    {
        [ReadOnly]
        public string id = Guid.NewGuid().ToString();
        public string key;

        public bool once;
        private BunnyEntryDescriptor descriptor;

        public BunnyBlackboardCriteria[] criteria;
        public BunnyBlackboardModificaiton[] modifications;

        public abstract BunnyEntryDescriptor GetDescriptor();
    }
}