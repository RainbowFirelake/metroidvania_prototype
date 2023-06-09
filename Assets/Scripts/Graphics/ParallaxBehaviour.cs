using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField, Range(0f, 1f)]
    private float _parallaxStrength = 0.1f;
    [SerializeField] private bool _disableVerticalParallax;
    
    private Vector3 _targetPreviousPosition;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        if (!_followingTarget)
        {
            _followingTarget = Camera.main.transform;
        }

        _targetPreviousPosition = _followingTarget.position;
    }

    private void Update()
    {
        var delta = _followingTarget.position - _targetPreviousPosition;

        if (_disableVerticalParallax)
        {
            delta.y = 0;
        }

        _targetPreviousPosition = _followingTarget.position;        
        _transform.position += delta * _parallaxStrength;
    }
}
