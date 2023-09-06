using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BasicMovement : MonoBehaviour
{
    public bool isGrounded { get { return _isGrounded; } }
    public bool CanDash { get { return _canDash; } }
    public bool IsDashing { get { return _isDashing; } }

    public event Action<float, float> OnMove;
    public event Action OnJump;
    public event Action<float> OnClimb;
    public event Action OnDash;

    protected float moveDirectionX;
    
    protected float moveDirectionY;

    protected bool _isGrounded = true;
    protected bool _canDash = true;
    protected bool _isDashing = false;

    public virtual void SetDirection(float directionX, float directionY)
    {
        moveDirectionX = Mathf.Clamp(directionX, -1, 1);
        moveDirectionY = Mathf.Clamp(directionY, -1, 1);
    }

    protected virtual void Move(float moveSpeedX, float moveSpeedY)
    {
        OnMove?.Invoke(moveSpeedX, moveSpeedY);
    }

    public void Jump(KeyState state = KeyState.Default)
    {
        JumpBehaviour(state);
        OnJump?.Invoke();
    }

    protected abstract void JumpBehaviour(KeyState state = KeyState.Default);

    public void Dash()
    {
        DashBehaviour();
        OnDash?.Invoke();
    }

    protected abstract void DashBehaviour();
    
    public abstract void DisableMove();
    
}
