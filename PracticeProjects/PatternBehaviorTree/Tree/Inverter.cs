namespace PatternBehaviorTree.Tree
{
    public class Inverter : BTNode
    {
        private BTNode _child;

        public Inverter(BTNode child)
        {
            _child = child;
        }

        public override NodeState Evaluate()
        {
            var state = _child.Evaluate();

            if (state == NodeState.Success)
                return NodeState.Failure;

            if (state == NodeState.Failure)
                return NodeState.Success;

            return NodeState.Running;
        }
    }
}
