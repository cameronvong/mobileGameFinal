using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [TODO] @mohammedajao - Add a custom editor for the table's data to be viewable in the editor
[CreateAssetMenu(menuName = "BDS/Data/Common")]
public class CommonTableSO: BunnyDialogueDatabaseCollection
{
    public CommonTableSO() : base("common")
    {
        // Facts
        AddFactEntry(new BunnyFactEntry(0, "enemies_killed", BunnyFactEntryScope.GLOBAL));
        AddFactEntry(new BunnyFactEntry(0, "player_deaths", BunnyFactEntryScope.GLOBAL));
        AddFactEntry(new BunnyFactEntry(0, "current_speakers", BunnyFactEntryScope.TEMPORARY));
    }

    private void OnEnable()
    {     
        // Events

        // Rules
    }
}