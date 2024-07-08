using Metroidvania.CharacterControllers;
using UnityEngine;

namespace Metroidvania.AI.Actions
{
    public class MoveToPointAIAction : IAction
    {
        public bool IsPointReached
        {
            get
            {
                if (_target == null)
                    return true;

                return Vector2.Distance(_target.position, _character.Position) < 1;
            }
        }
        private readonly MetroidvaniaCharacter _character;
        private Transform _target;

        public MoveToPointAIAction(MetroidvaniaCharacter character, Transform target)
        {
            _character = character;
            _target = target;
        }

        public void Execute()
        {
            if (IsPointReached)
            {
                // TODO: Stop() instead of MoveControllable(0, 0)
                _character.MoveControllable(0, 0);
                return;
            }

            if (_target.position.x < _character.Position.x)
            {
                _character.MoveControllable(-1, 0);
            }
            else if (_target.position.x > _character.Position.x)
            {
                _character.MoveControllable(1, 0);
            }
        }

        public void SetNewMovePoint(Transform newTarget)
        {
            _target = newTarget;
        }
    }
}
