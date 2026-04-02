using System;

namespace PatternBehaviorTree.Tree
{
    public class Condition : BTNode
    {
        private Func<bool> _condition;

        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }

        public override NodeState Evaluate()
        {
            return _condition() ? NodeState.Success : NodeState.Failure;
        }
    }
}
