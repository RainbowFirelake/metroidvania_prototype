using Metroidvania.AI.BehaviorTrees;
using System;

namespace Metroidvania.AI.ConcreteBehaviours
{
    public class Condition : IBehaviourStrategy
    {
        private readonly Func<bool> predicate;

        public Condition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public Node.Status Process() => predicate() ? Node.Status.Success : Node.Status.Failure;

        public void Reset() { }
    }
}
