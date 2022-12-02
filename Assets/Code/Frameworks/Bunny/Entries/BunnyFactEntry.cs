using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny.Entries
{    
    [CreateAssetMenu(fileName = "BunnyFactEntry", menuName = "Bunny/Entries/BunnyFactEntry", order = 0)]
    public class BunnyFactEntry : BunnyBaseEntry
    {
        public override BunnyEntryDescriptor GetDescriptor()
        {
            return BunnyEntryDescriptor.BunnyFactDescriptor;
        }
    }
}