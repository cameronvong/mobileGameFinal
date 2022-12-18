using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [TODO] @mohammedajao - Add a custom editor for the table's data to be viewable in the editor
[CreateAssetMenu(menuName = "BDS/Data/Speakers")]
public class SpeakersTableSO : BunnyDialogueDatabaseCollection
{
    public SpeakersTableSO() : base("speakers")
    {
        // Facts
        AddFactEntry(new BunnyFactEntry(0, "player", BunnyFactEntryScope.TEMPORARY));
        AddFactEntry(new BunnyFactEntry(0, "player_0", BunnyFactEntryScope.TEMPORARY));
        AddFactEntry(new BunnyFactEntry(0, "Guide-kun", BunnyFactEntryScope.TEMPORARY));
    }

    private void OnEnable()
    {
        
        // Events

        // Rules
    }
}