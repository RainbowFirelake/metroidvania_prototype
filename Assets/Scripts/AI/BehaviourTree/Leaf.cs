using System;

namespace Metroidvania.AI.BehaviorTrees
{
    public class Leaf : Node
    {
        private readonly IBehaviourStrategy _behaviourStrategy;

        public Leaf(string name, IBehaviourStrategy behaviourStrategy, int priority = 0) : base(name, priority)
        {
            _behaviourStrategy = behaviourStrategy ?? throw new ArgumentNullException();
        }

        public override Status Process() => _behaviourStrategy.Process();

        public override void Reset() => _behaviourStrategy.Reset();
    }
}
