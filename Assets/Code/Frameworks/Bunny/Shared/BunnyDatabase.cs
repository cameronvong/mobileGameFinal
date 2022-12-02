using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Blackboards;
using Bunny.Entries;

namespace Bunny.Shared
{

    public interface IBunnyDatabase
    {
        IBunnyBlackboard GetBlackboardForEntry(string entry);
        void ApplyEntry(BunnyBaseEntry entry, BunnyTable table);
        bool TryGetEntry(string id, out BunnyBaseEntry candidate);
        bool TryGetRule(string id, IBunnyBlackboard context, out BunnyRuleEntry match);
        bool TryGetTable(string id, out BunnyTable table);
        bool TestEntry(BunnyBaseEntry entry, IBunnyBlackboard context);
        IBunnyBlackboard GetBlackboard(int scope, IBunnyBlackboard context);
        BunnyFactEntry GenerateFact(string key, IBunnyBlackboard context);
        BunnyEventEntry GenerateEvent(string key);
        BunnyRuleEntry GenerateRule(string key);
    }

    [CreateAssetMenu(fileName="BunnyDatabase", menuName="Bunny/Dialogue/Database", order = 0)]
    public class BunnyDatabase : ScriptableObject
    {
        public List<IBunnyBlackboard> blackboards;
    }
}