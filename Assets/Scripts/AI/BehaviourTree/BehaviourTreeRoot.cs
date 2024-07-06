namespace Metroidvania.AI.BehaviorTree
{
    public class BehaviourTreeRoot : Node
    {
        public BehaviourTreeRoot(string name) : base(name) { }

        public override Status Process()
        {
            while (currentChild < children.Count)
            {
                var status = children[currentChild].Process();
                if (status != Status.Success)
                {
                    return status;
                }
                currentChild++;
            }

            return Status.Success;
        }
    }
}
