using Metroidvania.AI.BehaviorTree;
using Metroidvania.CharacterControllers;
using System.Collections.Generic;
using UnityEngine;

namespace Metroidvania.AI.ConcreteBehaviours
{
    [System.Serializable]
    public class PatrolStrategy : IBehaviourStrategy
    {
        private readonly Transform _transform;
        private readonly MetroidvaniaCharacter _character;
        private readonly List<Transform> _patrolPoints;
        private int _currentIndex;

        public PatrolStrategy(
            Transform transform,
            MetroidvaniaCharacter character,
            List<Transform> patrolPoints)
        {
            _transform = transform;
            _character = character;
            _patrolPoints = patrolPoints;
            _currentIndex = 0;
        }

        public Node.Status Process()
        {
            if (_currentIndex == _patrolPoints.Count)
            {
                _currentIndex = 0;
                return Node.Status.Success;
            }

            var target = _patrolPoints[_currentIndex];

            if (Vector2.Distance(target.position, _transform.position) < 1)
            {
                _currentIndex++;
            }

            if (target.position.x < _transform.position.x)
            {
                _character.MoveControllable(-1, 0);
            }
            else if (target.position.x > _transform.position.x)
            {
                _character.MoveControllable(1, 0);
            }

            return Node.Status.Running;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}
