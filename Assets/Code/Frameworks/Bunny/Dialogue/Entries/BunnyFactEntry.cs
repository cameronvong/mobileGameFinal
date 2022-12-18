using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

/*
Used to get facts of the world.
EG player_visited_npc_count = 0
*/
public enum BunnyFactEntryScope 
{
    GLOBAL,
    AREA,
    SCENE,
    TEMPORARY
}
public class BunnyFactEntry : BunnyBaseEntry
{
    private BunnyFactEntryScope scope;

    [SerializeField]
    public BunnyFactEntryScope Scope => scope;

    public BunnyFactEntry(int ID, string key, BunnyFactEntryScope newScope = BunnyFactEntryScope.TEMPORARY)
    {
        this.ID = ID;
        this.Key = key;
        this.scope = newScope;
    }
}