namespace PatternBehaviorTree.Tree
{
    public abstract class BTNode
    {
        public enum NodeState
        {
            Running,
            Success,
            Failure
        }

        public abstract NodeState Evaluate();
    }
}
