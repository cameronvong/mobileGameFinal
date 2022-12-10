using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Entries;
using Bunny.Blackboards;
using Bunny.Shared;

namespace Bunny.Blackboards
{
    public enum BunnyFactOperation
    {
        SET,
        INCREMENT
    }

    [Serializable]
    public class BunnyBlackboardModificaiton
    {
        public BunnyBaseEntry entry;
        public BunnyFactOperation operation = BunnyFactOperation.SET;

        public int input;

        public void Execute(BunnyDatabase database)
        {
            IBunnyBlackboard blackboard = database.GetBlackboardForEntry(entry);
            int value = blackboard.Get(entry.id);

            switch(operation) {
                case BunnyFactOperation.SET:
                    blackboard.Set(entry.id, input);
                    break;
                case BunnyFactOperation.INCREMENT:
                    blackboard.Set(entry.id, input + value);
                    break;
                default:
                    break;
            }
        }
    }
}