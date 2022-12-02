using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public string entryId;

        public BunnyFactOperation operation = BunnyFactOperation.SET;

        public int input;

        public void Execute(IBunnyDatabase database)
        {
            IBunnyBlackboard blackboard = database.GetBlackboardForEntry(entryId);
            int value = blackboard.Get(entryId);

            switch(operation) {
                case BunnyFactOperation.SET:
                    blackboard.Set(entryId, input);
                    break;
                case BunnyFactOperation.INCREMENT:
                    blackboard.Set(entryId, input + value);
                    break;
                default:
                    break;
            }
        }
    }
}