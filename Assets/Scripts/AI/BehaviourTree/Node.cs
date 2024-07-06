using System.Collections.Generic;

namespace Metroidvania.AI.BehaviorTree
{
    public class Node
    {
        public enum Status { Success, Failure, Running }

        public readonly string name;

        public readonly List<Node> children = new();
        protected int currentChild;

        public Node(string name = "Node")
        {
            this.name = name;
        }

        public void AddChild(Node child) => children.Add(child);

        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (var child in children)
            {
                child.Reset();
            }
        }
    }
}
