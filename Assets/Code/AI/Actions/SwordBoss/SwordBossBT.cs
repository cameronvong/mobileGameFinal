using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;

public class SwordBossBT: BTTree
{
    protected override BTNode SetupTree()
    {
        BTNode root = new SwordBossFloat(transform, body);
        return root;
    }
}