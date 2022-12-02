using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.BehaviourTree {
    public class BTSequence : BTNode
    {
        public override BTNodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (BTNode node in children)
            {
                switch(node.Evaluate())
                {
                    case BTNodeState.FAILURE:
                        state = BTNodeState.FAILURE;
                        return state; 
                    case BTNodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    case BTNodeState.SUCCESS:
                        continue;
                    default:
                        state = BTNodeState.SUCCESS;
                        return state;
                } 
            }
            state = anyChildIsRunning ? BTNodeState.RUNNING : BTNodeState.SUCCESS;
            return state;
        }
    }
}