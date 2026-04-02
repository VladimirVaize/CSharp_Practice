using System;

namespace PatternBehaviorTree.Tree
{
    public class ActionNode : BTNode
    {
        private Func<NodeState> _action;

        public ActionNode(Func<NodeState> action)
        {
            _action = action;
        }

        public override NodeState Evaluate()
        {
            return _action();
        }
    }
}
