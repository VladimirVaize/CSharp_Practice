using System.Collections.Generic;

namespace PatternBehaviorTree.Tree
{
    public class Selector : BTNode
    {
        private List<BTNode> _children;

        public Selector(List<BTNode> children)
        {
            _children = children;
        }

        public override NodeState Evaluate()
        {
            foreach (var child in _children)
            {
                var state = child.Evaluate();

                if (state == NodeState.Success)
                    return NodeState.Success;

                if (state == NodeState.Running)
                    return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
