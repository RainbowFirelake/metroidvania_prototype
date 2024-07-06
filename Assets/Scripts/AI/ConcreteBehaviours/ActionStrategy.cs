using Metroidvania.AI.BehaviorTree;
using System;

namespace Metroidvania.AI.ConcreteBehaviours
{
    public class ActionStrategy : IBehaviourStrategy
    {
        private readonly Action _action;

        public ActionStrategy(Action action)
        {
            _action = action;
        }

        public Node.Status Process()
        {
            _action();
            return Node.Status.Success;
        }

        public void Reset() { }
    }
}
