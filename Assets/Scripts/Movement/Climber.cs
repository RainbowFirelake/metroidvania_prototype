using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    [SerializeField] private WallDetector _leftDetector;
    [SerializeField] private WallDetector _rightDetector;
    [SerializeField] private MoveParams _stats;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public bool isClimbing { get; private set; } = false;

    private void OnEnable()
    {
        _leftDetector.OnWallEnter += EnableClimbing;
        _leftDetector.OnWallExit += DisableClimbing;
        _rightDetector.OnWallEnter += EnableClimbing;
        _rightDetector.OnWallExit += DisableClimbing;
    }

    private void OnDisable()
    {
        _leftDetector.OnWallEnter -= EnableClimbing;
        _leftDetector.OnWallExit -= DisableClimbing;
        _rightDetector.OnWallEnter -= EnableClimbing;
        _rightDetector.OnWallExit -= DisableClimbing;
    }

    public void Climb(float directionY)
    {
        if (!isClimbing) return;

        var vector = new Vector2(
            _rigidbody2D.velocity.x,
            _stats.ClimbSpeed * directionY);
        
        _rigidbody2D.velocity = vector;
    }

    private void EnableClimbing()
    {
        isClimbing = true;
    }

    private void DisableClimbing()
    {
        isClimbing = false;
    }
}
