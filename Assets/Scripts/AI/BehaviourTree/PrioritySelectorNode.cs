using System.Collections.Generic;
using System.Linq;

namespace Metroidvania.AI.BehaviorTrees
{
    public class PrioritySelectorNode : Node
    {
        List<Node> sortedChildren;
        List<Node> SortedChildren => sortedChildren ??= SortChildren();

        public PrioritySelectorNode(string name) : base(name) { }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        return Status.Success;
                    default:
                        continue;
                }
            }

            return Status.Failure;
        }

        public override void Reset()
        {
            base.Reset();
            sortedChildren = null;
        }

        protected virtual List<Node> SortChildren() =>
            children.OrderByDescending(child => child.priority).ToList();
    }
}
