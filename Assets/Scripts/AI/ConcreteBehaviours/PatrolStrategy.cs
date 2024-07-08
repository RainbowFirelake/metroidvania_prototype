using Metroidvania.AI.Actions;
using Metroidvania.AI.BehaviorTrees;
using Metroidvania.CharacterControllers;
using System.Collections.Generic;
using UnityEngine;

namespace Metroidvania.AI.ConcreteBehaviours
{
    [System.Serializable]
    public class PatrolStrategy : IBehaviourStrategy
    {
        private readonly MetroidvaniaCharacter _character;
        private readonly List<Transform> _patrolPoints;
        private int _currentIndex;

        private MoveToPointAIAction _mover;
        private IActionExecutor _executor;

        public PatrolStrategy(
            MetroidvaniaCharacter character,
            List<Transform> patrolPoints,
            IActionExecutor executor)
        {
            _character = character;
            _patrolPoints = patrolPoints;
            _currentIndex = 0;
            _executor = executor;
            _mover = new MoveToPointAIAction(_character, _patrolPoints[0]);
        }

        public Node.Status Process()
        {
            Debug.Log(_currentIndex);

            if (_currentIndex == _patrolPoints.Count)
            {
                _executor.StopAction();
                _currentIndex = 0;
                return Node.Status.Success;
            }

            _executor.SetAction(_mover);

            _mover.SetNewMovePoint(_patrolPoints[_currentIndex]);

            if (_mover.IsPointReached)
            {
                _currentIndex++;
            }

            return Node.Status.Running;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}
