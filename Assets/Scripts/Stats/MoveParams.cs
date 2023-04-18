using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats System/New Movement Params", fileName = "NewMovementParams", order = 2)]
public class MoveParams : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _moveSpeedInAir = 3f;

    [SerializeField] private float _jumpStrength = 10f;
    [SerializeField] private bool _isMovementActiveWhenInAir = true;
    // Время, во время которого ещё можно прыгнуть после выхода с платформы
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _jumpBufferingTime = 0.5f;

    [SerializeField] private float _dashStrength = 3f;
    [SerializeField] private float _dashTime = 1f;
    [SerializeField] private float _dashCooldown = 1f;

    [SerializeField] private bool _canClimbOnWalls = true;
    [SerializeField] private float _climbSpeed = 4f;

    public float MoveSpeed { get { return _moveSpeed; } }
    public float MoveSpeedInAir { get { return _moveSpeedInAir; } }

    public float ClimbSpeed { get { return _climbSpeed; } }

    public float JumpStrength { get { return _jumpStrength; } }
    public float CoyoteTime { get { return _coyoteTime; } }
    public float JumpBufferingTime { get { return _jumpBufferingTime; } }

    public float DashStrength { get { return _dashStrength; } }
    public float DashTime { get { return _dashTime; } }
    public float DashCooldown { get { return _dashCooldown; } }
 
    public bool CanClimbOnWalls { get { return _canClimbOnWalls; } }
    public bool IsMovementActiveWhenInAir { get { return _isMovementActiveWhenInAir; } }
}
