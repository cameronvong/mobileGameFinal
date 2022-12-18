using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class BunnyDialogueDatabaseException : Exception
{
    public BunnyDialogueDatabaseException() {}
    public BunnyDialogueDatabaseException(string message) {}
}

/*
ABOUT:
The Dialogue Manager is the brain of the dialogue service.
All fact references lie here, are created here, and mutated here.

Creating a fact outside the Manager is highly decentivized. 
Facts created outside the system essentially "do not exist" as they cannot
be observed by the system.
*/

public class BunnyDialogueManager
{
    public Dictionary<string, BunnyDialogueDatabaseCollection> Database = new Dictionary<string, BunnyDialogueDatabaseCollection>();
    public DialogueSpeakerMap Speakers = (DialogueSpeakerMap) ScriptableObject.CreateInstance("DialogueSpeakerMap");

    private static BunnyDialogueManager _instance;
    private static readonly object padlock = new object();
    private BunnyDialogueManager() {}

    public static BunnyDialogueManager Instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null) {
                    _instance = new BunnyDialogueManager();
                }
                return _instance;
            }
        }
    }

    public void Initialize()
    {
        CommonTableSO commonTable = (CommonTableSO) ScriptableObject.CreateInstance("CommonTableSO");
        commonTable.Initialize();
        RegisterTable(commonTable.Name, commonTable);

        SpeakersTableSO speakersTable = (SpeakersTableSO) ScriptableObject.CreateInstance("SpeakersTableSO");
        speakersTable.Initialize();
        Database.Add(speakersTable.Name, speakersTable);


        DemoTableSO demoTable = (DemoTableSO) ScriptableObject.CreateInstance("DemoTableSO");
        demoTable.Initialize();
        Database.Add(demoTable.Name, demoTable);
    }

    public void RegisterTable(string TableName, BunnyDialogueDatabaseCollection Table)
    {
        if(Database.ContainsKey(TableName))
            throw new BunnyDialogueDatabaseException($"Table: {TableName} already exists. Do not attempt to overwrite.");
        Database.Add(TableName, Table);
    }

    public void UnregisterTable(string TableName)
    {
        if(Database.ContainsKey(TableName))
            Database.Remove(TableName);
    }

    public BunnyFactEntry GetFact(string TableName, string FactKey)
    {
        return Database[TableName].GetFact(FactKey);
    }

    public BunnyEventEntry GetEvent(string TableName, string Key)
    {
        return Database[TableName].GetEntry<BunnyEventEntry>(Key);
    }

    public BunnyRuleEntry GetRule(string TableName, string Key)
    {
        return Database[TableName].GetEntry<BunnyRuleEntry>(Key);
    }

}
