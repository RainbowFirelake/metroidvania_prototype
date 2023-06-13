using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class AIController : MonoBehaviour
{
    [SerializeField] private AIData _aiData;
    [SerializeField] IControllable _controllable;
    [SerializeField] private AllyAndEnemySystem _allyAndEnemy;
    [SerializeField] private DetectionArea _detectionArea;

    private List<AllyAndEnemySystem> _detectedCharacters;
    private AllyAndEnemySystem _nearestEnemy;
    private Transform _transform;
    
    private float _timeAfterLastBehaviourUpdate = 0;
    private float _timeAfterLastAttack = 0;

    void Start()
    {
        _detectedCharacters = new List<AllyAndEnemySystem>();
        _transform = GetComponent<Transform>();
        _controllable = GetComponent<IControllable>();
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
        UpdateTimers();

        if (_timeAfterLastBehaviourUpdate > _aiData.timeOfUpdatingBehaviours)
        {
            _timeAfterLastBehaviourUpdate = 0f;
            
            FindNearestEnemy();
            if (IdleBehaviour()) return;
            if (ChaseBehaviour()) return;
            if (AttackBehaviour()) return;
        }
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

    private bool ChaseBehaviour()
    {
        var distance = Vector2.Distance(_transform.position,
            _nearestEnemy.transform.position);

        if (distance > _aiData.attackDistance)
        {
            var enemyPoint = GetEnemyPoint(_nearestEnemy);

            if (enemyPoint < 0)
            {
                _controllable.MoveControllable(-1f, 0f);
            }
            if (enemyPoint > 0)
            {
                _controllable.MoveControllable(1f, 0f);
            }
            return true;
        } 

        return false; 
    }

    private bool IdleBehaviour()
    {
        if (!_nearestEnemy) 
        {
            _controllable.MoveControllable(0f, 0f); 
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
                _controllable.MoveControllable(0f, 0f);
                _controllable.Attack();
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
}