using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.BehaviourTree {
    public class BTSelector : BTNode
    {
        public BTSelector(): base() {}
        public BTSelector(List<BTNode> children): base(children) {}
        public override BTNodeState Evaluate()
        {
            foreach (BTNode node in children)
            {
                switch(node.Evaluate())
                {
                    case BTNodeState.FAILURE:
                        continue; 
                    case BTNodeState.SUCCESS:
                        state = BTNodeState.SUCCESS;
                        return state;
                    case BTNodeState.RUNNING:
                        state = BTNodeState.RUNNING;
                        return state;
                    default:
                        continue;
                } 
            }
            state = BTNodeState.FAILURE;
            return state;
        }
    }
}