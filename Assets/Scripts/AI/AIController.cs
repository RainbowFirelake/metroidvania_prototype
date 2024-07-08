using Metroidvania.AI.Actions;
using Metroidvania.AI.BehaviorTrees;
using Metroidvania.AI.ConcreteBehaviours;
using Metroidvania.CharacterControllers;
using System.Collections.Generic;
using UnityEngine;

namespace Metroidvania.AI
{
    [RequireComponent(typeof(IControllable))]
    public class AIController : MonoBehaviour, IActionExecutor
    {
        public Transform NextMovePoint { get; private set; }

        public IAction Action { get; private set; }

        [SerializeField] private AIData _aiData;
        [SerializeField] MetroidvaniaCharacter _character;
        [SerializeField] private AllyAndEnemySystem _allyAndEnemy;
        [SerializeField] private DetectionArea _detectionArea;

        [SerializeField] private List<Transform> _patrolPoints;

        private List<AllyAndEnemySystem> _detectedCharacters;
        [SerializeField]
        private AllyAndEnemySystem _nearestEnemy;
        private Transform _transform;

        private BehaviourTree _behaviourTree;

        private float _timeAfterLastBehaviourUpdate = 0;
        private float _timeAfterLastAttack = 0;

        private void OnValidate()
        {
            _transform = GetComponent<Transform>();
            _character = GetComponent<MetroidvaniaCharacter>();
        }

        private void Awake()
        {
            _behaviourTree = new BehaviourTree("AIController");
            _behaviourTree.AddChild(new Leaf("Patrol", new PatrolStrategy(_character, _patrolPoints, this)));

            //var isEnemyAlive = new Leaf("IsEnemyAlive", new Condition(() => _nearestEnemy != null));
            //var moveToEnemy = new Leaf("MoveToEnemy", new ActionStrategy(() => MoveToPoint(_nearestEnemy.transform)));

            //var sequence = new SequenceNode("GoToEnemy");

            //sequence.AddChild(isEnemyAlive);
            //sequence.AddChild(moveToEnemy);

            //_behaviourTree.AddChild(sequence);
        }

        void Start()
        {
            _detectedCharacters = new List<AllyAndEnemySystem>();
        }

        void OnEnable()
        {
            _detectionArea.OnCharacterEnter += AddToDetectedCharacters;
            _detectionArea.OnCharacterExit += DeleteFromDetectedCharacters;
        }

        void OnDisable()
        {
            _detectionArea.OnCharacterEnter -= AddToDetectedCharacters;
            _detectionArea.OnCharacterExit -= DeleteFromDetectedCharacters;
        }

        void Update()
        {
            _behaviourTree.Process();
            Action?.Execute();

            //UpdateTimers();

            //if (_timeAfterLastBehaviourUpdate > _aiData.timeOfUpdatingBehaviours)
            //{
            //    _timeAfterLastBehaviourUpdate = 0f;

            //    FindNearestEnemy();
            //    if (IdleBehaviour()) return;
            //    if (ChaseBehaviour()) return;
            //    if (AttackBehaviour()) return;
            //}
        }

        // private IEnumerator UpdateBehaviours()
        // {
        //     while (true)
        //     {
        //         FindNearestEnemy();
        //         if (IdleBehaviour()) yield return new WaitForSeconds(_aiData.timeOfUpdatingBehaviours);
        //         if (ChaseBehaviour()) yield return new WaitForSeconds(_aiData.timeOfUpdatingBehaviours);
        //         if (AttackBehaviour()) yield return new WaitForSeconds(_aiData.timeOfUpdatingBehaviours);
        //     }
        // }

        private bool IdleBehaviour()
        {
            if (!_nearestEnemy)
            {
                _character.MoveControllable(0f, 0f);
                return true;
            }

            return false;
        }

        private bool AttackBehaviour()
        {
            var distance = Vector2.Distance(_transform.position,
                _nearestEnemy.transform.position);

            if (distance < _aiData.attackDistance)
            {
                if (_timeAfterLastAttack > _aiData.attackDistance)
                {
                    _character.MoveControllable(0f, 0f);
                    _character.Attack();
                }

                return true;
            }

            return false;
        }

        private void FindNearestEnemy()
        {
            if (_detectedCharacters.Count == 0)
            {
                _nearestEnemy = null;
            }
            float minimalDistance = Mathf.Infinity;
            AllyAndEnemySystem enemyToReturn = null;
            foreach (var enemy in _detectedCharacters)
            {
                float distance = Vector2.Distance(_transform.position,
                    enemy.transform.position);

                if (enemy.characterSide != _allyAndEnemy.characterSide
                    && minimalDistance > distance)
                {
                    enemyToReturn = enemy;
                    minimalDistance = distance;
                }
            }

            _nearestEnemy = enemyToReturn;
        }

        private float GetEnemyPoint(AllyAndEnemySystem enemy)
        {
            if (enemy.transform.position.x < _transform.position.x)
            {
                return -1;
            }
            else return 1;
        }

        private void AddToDetectedCharacters(AllyAndEnemySystem character)
        {
            if (!_detectedCharacters.Contains(character))
            {
                _detectedCharacters.Add(character);
            }
        }

        private void DeleteFromDetectedCharacters(AllyAndEnemySystem character)
        {
            if (_detectedCharacters.Contains(character))
            {
                _detectedCharacters.Remove(character);
            }
        }

        private void UpdateTimers()
        {
            _timeAfterLastBehaviourUpdate += Time.deltaTime;
            _timeAfterLastAttack += Time.deltaTime;
        }

        public void SetAction(IAction action)
        {
            Action = action;
        }

        public void StopAction()
        {
            Action = null;
        }
    }
}
