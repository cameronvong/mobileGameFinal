using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;

// [TODO] @mohammedajao - Add a custom editor for the table's data to be viewable in the editor
[CreateAssetMenu(menuName = "BDS/Data/Demo")]
public class DemoTableSO : BunnyDialogueDatabaseCollection
{
    public DemoTableSO() : base("demo")
    {
        // Facts
        AddFactEntry(new BunnyFactEntry(0, "has_jumped", BunnyFactEntryScope.SCENE));
        AddFactEntry(new BunnyFactEntry(0, "convo_on_talk_inits", BunnyFactEntryScope.SCENE));
    }

    public override void Initialize()
    {
        // Events
        BunnyDialogueCriteria[] on_talk_policies = {
            new BunnyDialogueCriteria("has_jumped", "demo"),
            new BunnyDialogueCriteria("player_deaths", "common", BunnyCriteriaComparator.LESS_THAN_OR_EQUAL_TO, 5)
        };
        
        AddEventEntry(new BunnyEventEntry(
            "on_talk",
            on_talk_policies,
            BunnyEventManager.Instance.RegisterEvent("demo_on_talk", this)
        ));

        // Rules
        BunnyDialogueCriteria[] on_talk_rule_policies = {
            new BunnyDialogueCriteria("has_jumped", "demo")
        };
        BunnyEntryModification[] on_talk_rule_modifications = {
            new BunnyEntryModification(
                this.Facts["convo_on_talk_inits"],
                1,
                BunnyModificationType.INCREASE
            )
        };

        AddRuleEntry(new BunnyRuleEntry(
            "instructions_1",
            on_talk_rule_policies,
            this.Events["on_talk"],
            BunnyDialogueManager.Instance.GetFact("speakers", "Guide-kun"),
            "You can jump using the spacebar. Try it!",
            this.Events["on_talk"],
            on_talk_rule_modifications
        ));

        BunnyDialogueCriteria[] on_talk_b_rule_policies = {
            new BunnyDialogueCriteria("on_talk", "demo", BunnyCriteriaComparator.EQUALS, 1, BunnyEntryType.EVENT),
            new BunnyDialogueCriteria("has_jumped", "demo")
        };
        
        AddRuleEntry(new BunnyRuleEntry(
            "instructions_2",
            on_talk_b_rule_policies,
            this.Events["on_talk"],
            BunnyDialogueManager.Instance.GetFact("speakers", "Guide-kun"),
            "You know...It's weird you haven't jumped yet!",
            on_talk_rule_modifications
        ));
    }

    private void OnEnable()
    {
        Debug.Log("Enabled called");
        // Events
        BunnyDialogueCriteria[] on_talk_policies = {
            new BunnyDialogueCriteria("has_jumped", "demo"),
            new BunnyDialogueCriteria("player_deaths", "common", BunnyCriteriaComparator.LESS_THAN_OR_EQUAL_TO, 5)
        };
        
        AddEventEntry(new BunnyEventEntry(
            "on_talk",
            on_talk_policies,
            BunnyEventManager.Instance.RegisterEvent("demo_on_talk_test", this)
        ));

        // Rules
        BunnyDialogueCriteria[] on_talk_rule_policies = {
            new BunnyDialogueCriteria("has_jumped", "demo")
        };
        BunnyEntryModification[] on_talk_rule_modifications = {
            new BunnyEntryModification(
                this.Events["on_talk"],
                1,
                BunnyModificationType.INCREASE
            )
        };

        Debug.Log(BunnyDialogueManager.Instance.GetFact("speakers", "Guide-kun") != null);

        AddRuleEntry(new BunnyRuleEntry(
            "instructions_1",
            on_talk_rule_policies,
            this.Events["on_talk"],
            BunnyDialogueManager.Instance.GetFact("speakers", "Guide-kun"),
            "You can jump using the spacebar. Try it!",
            on_talk_rule_modifications
        ));
    }
}

