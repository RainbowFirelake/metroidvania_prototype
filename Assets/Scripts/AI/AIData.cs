using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIData", menuName = "new AIData", order = 2)]
public class AIData : ScriptableObject
{
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _timeBetweenAttacks = 1f;
    [SerializeField] private float _timeOfUpdatingBehaviours = 0.2f;
    
    public float attackDistance { get { return _attackDistance; } } 
    public float timeBetweenAttacks { get { return _timeBetweenAttacks; } }
    public float timeOfUpdatingBehaviours{ get { return _timeOfUpdatingBehaviours; } }
} 
