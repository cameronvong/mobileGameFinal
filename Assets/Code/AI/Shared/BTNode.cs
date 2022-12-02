using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree {
    public enum BTNodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class BTNode
    {
        protected BTNodeState state;

        public BTNode parent;
        protected List<BTNode> children;

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public BTNode() { parent = null; }

        public BTNode(List<BTNode> kids)
        {
            foreach (BTNode child in kids)
                _Attach(child);
        }

        private void _Attach(BTNode node)
        {
            node.parent = this;
            children.Add(this);
        }

        public virtual BTNodeState Evaluate() => BTNodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;
            BTNode node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool DeleteData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            BTNode node = parent;
            while(node != null)
            {
                bool cleared = node.DeleteData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}