using System.Collections.Generic;

namespace PatternBehaviorTree.Tree
{
    public class Sequence : BTNode
    {
        private List<BTNode> _children;

        public Sequence(List<BTNode> children)
        {
            _children = children;
        }

        public override NodeState Evaluate()
        {
            foreach (var child in _children)
            {
                var state = child.Evaluate();

                if (state == NodeState.Failure)
                    return NodeState.Failure;

                if (state == NodeState.Running)
                    return NodeState.Running;
            }

            return NodeState.Success;
        }
    }
}
