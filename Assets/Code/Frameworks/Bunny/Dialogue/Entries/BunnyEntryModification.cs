using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyModificationType
{
    INCREASE,
    DECREASE,
    SET
}

public class BunnyEntryModification
{
    [SerializeField]
    public int Value;

    [SerializeField]
    BunnyBaseEntry Fact;

    [SerializeField]
    BunnyModificationType ModificationType;

    public BunnyEntryModification(
        BunnyBaseEntry Entry,
        int Data,
        BunnyModificationType ModType = BunnyModificationType.SET
    )
    {
        Fact = Entry;
        Value = Data;
        ModificationType = ModType;
    }

    public void Modify()
    {
        switch(ModificationType)
        {
            case BunnyModificationType.INCREASE:
                Fact.ID += Value;
                return;
            case BunnyModificationType.DECREASE:
                Fact.ID -= Value;
                return;
            default:
                Fact.ID = Value;
                return;
        }
    }
}
