using Metroidvania.AI.Actions;
using Metroidvania.AI.BehaviorTrees;
using Metroidvania.CharacterControllers;
using UnityEngine;

namespace Metroidvania.AI.ConcreteBehaviours
{
    public class MoveToPointStrategy : IBehaviourStrategy
    {
        private MetroidvaniaCharacter _character;
        private Transform _target;
        private IActionExecutor _executor;
        private readonly MoveToPointAIAction _mover;

        public MoveToPointStrategy(
            MetroidvaniaCharacter character,
            Transform target,
            IActionExecutor executor)
        {
            _character = character;
            _target = target;
            _executor = executor;
            _mover = new MoveToPointAIAction(character, target);
        }

        public Node.Status Process()
        {
            _executor.SetAction(_mover);

            _mover.SetNewMovePoint(_target);

            if (_mover.IsPointReached)
            {
                _character.MoveControllable(0, 0);
                return Node.Status.Success;
            }

            return Node.Status.Running;
        }

        public void Reset() { }
    }
}
