using System;

namespace Metroidvania.AI.BehaviorTree
{
    public class Leaf : Node
    {
        private readonly IBehaviourStrategy _behaviourStrategy;

        public Leaf(string name, IBehaviourStrategy behaviourStrategy) : base(name)
        {
            this._behaviourStrategy = behaviourStrategy ?? throw new ArgumentNullException();
        }

        public override Status Process() => _behaviourStrategy.Process();

        public override void Reset() => _behaviourStrategy.Reset();
    }
}
