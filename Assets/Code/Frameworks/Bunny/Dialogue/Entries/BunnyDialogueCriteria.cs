using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consists of a fact
// one or two comparator
// 2 values for testing

public enum BunnyCriteriaComparator {
  EXISTS,
  EQUALS,
  LESS_THAN,
  GREATER_THAN,
  LESS_THAN_OR_EQUAL_TO,
  GREATER_THAN_OR_EQUAL_TO
};

public class BunnyDialogueCriteria
{
  [SerializeField]
  public string FactKey;

  [SerializeField]
  public string Table;

  [SerializeField]
  public BunnyCriteriaComparator Comparator;

  [SerializeField]
  public int Value;

  [SerializeField]
  private bool Dummy = false;

  [SerializeField]
  public BunnyEntryType EntryType;

  public BunnyDialogueCriteria None
  {
    get
    {
      return new BunnyDialogueCriteria("Dummy", "Common", true);
    }
  }

  private BunnyDialogueCriteria(
    string key,
    string collection,
    bool isDummy,
    BunnyCriteriaComparator op = BunnyCriteriaComparator.EQUALS,
    int val = 0
  )
  {
    FactKey = key;
    Table = collection;
    Comparator = op;
    Value = val;
    EntryType = BunnyEntryType.FACT;
  }

  public BunnyDialogueCriteria(
    string key,
    string collection,
    BunnyCriteriaComparator op = BunnyCriteriaComparator.EQUALS,
    int val = 0,
    BunnyEntryType entryTypeEnum = BunnyEntryType.FACT
  )
  {
    FactKey = key;
    Table = collection;
    Comparator = op;
    Value = val;
    EntryType = entryTypeEnum;
  }

  public bool IsSatisfied()
  {
    if(FactKey == "Dummy" || Dummy)
      return true;
    // Will search DB tables for current fact status
    //  If fact does not exist, we return false
    BunnyBaseEntry fact;

    if(EntryType == BunnyEntryType.FACT)
    {
      fact = BunnyDialogueManager.Instance.GetFact(Table, FactKey);
    }
    else if(EntryType == BunnyEntryType.EVENT)
    {
      fact = BunnyDialogueManager.Instance.GetEvent(Table, FactKey);
    }
    else if (EntryType == BunnyEntryType.RULE)
    {
      fact = BunnyDialogueManager.Instance.GetRule(Table, FactKey);
    } else {
      return false;
    }

    switch(Comparator)
    {
      case BunnyCriteriaComparator.EQUALS:
        return fact.ID == Value;
      case BunnyCriteriaComparator.LESS_THAN:
        return fact.ID < Value;
      case BunnyCriteriaComparator.GREATER_THAN:
        return fact.ID > Value;
      case BunnyCriteriaComparator.LESS_THAN_OR_EQUAL_TO:
        return fact.ID <= Value;
      case BunnyCriteriaComparator.GREATER_THAN_OR_EQUAL_TO:
        return fact.ID >= Value;
      default:
        return true;
    }
  }
}
