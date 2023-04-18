using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IControllable
{
    [SerializeField] private BasicMovement _movement;
    [SerializeField] private BasicCombat _combat;

    private bool _canMove = true;

    private void OnEnable()
    {
        _combat.OnAttackStart += DisableMovement;
        _combat.OnAttackEnd += EnableMovement;
    }

    public void MoveControllable(float horizontalInput, float verticalInput)
    {
        if (!_canMove)
        {
            StopMove();
            return;
        }

        _movement.SetDirection(horizontalInput, verticalInput);
    }

    public void Attack()
    {
        if (!_movement.isGrounded || _movement.IsDashing) return;

        _combat.Attack();
    }

    public void Block()
    {
        _combat.Block(true);
    }

    public void Dash()
    {
        _movement.Dash();
    }

    public void Jump()
    {
        _movement.Jump();
    }

    private void StopMove()
    {
        _movement.DisableMove();
    }

    private void EnableMovement()
    {
        _canMove = true;
        _movement.EnableMove();
    }

    private void DisableMovement()
    {
        _canMove = false;
        StopMove();
    }
}
