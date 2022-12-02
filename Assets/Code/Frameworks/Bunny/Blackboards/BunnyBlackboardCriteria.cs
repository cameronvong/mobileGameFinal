using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Entries;
using Bunny.Shared;

namespace Bunny.Blackboards
{
    public enum BunnyFactComparator
    {
        EQUALS_TO,
        LESS_THAN,
        LESS_THAN_EQ,
        GREATER_THAN,
        GREATER_THAN_EQ,
        EXISTS
    }

    [Serializable]
    [CreateAssetMenu(fileName="Dialogue Criteria", menuName="Bunny/Dialogue/Dialogue Criteria")]
    public class BunnyBlackboardCriteria : ScriptableObject
    {
        public string entryId;

        public BunnyFactComparator comparator = BunnyFactComparator.EQUALS_TO;

        public int input;

        public bool Test(IBunnyDatabase database)
        {
            int value = database.GetBlackboardForEntry(entryId).Get(entryId);
            bool entryExists = database.TryGetEntry(entryId, out BunnyBaseEntry candidate);

            if(!entryExists) return false;
        
            switch(comparator) {
                case BunnyFactComparator.EQUALS_TO:
                    return value == input;
                case BunnyFactComparator.LESS_THAN:
                    return value < input;
                case BunnyFactComparator.LESS_THAN_EQ:
                    return value <= input;
                case BunnyFactComparator.GREATER_THAN:
                    return value > input;
                case BunnyFactComparator.GREATER_THAN_EQ:
                    return value >= input;
                case BunnyFactComparator.EXISTS:
                    return entryExists;
                default:
                    return false;
            }
        }
    }
}