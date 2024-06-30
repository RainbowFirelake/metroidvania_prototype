using System;
using UnityEngine;

public abstract class BasicMovementBehaviour : MonoBehaviour
{
    public event Action<float, float> OnMove;

    public Vector2 MovementDirection { get; private set; } = Vector2.zero;

    public virtual void SetDirection(float directionX, float directionY)
    {
        MovementDirection = new Vector2(
            Mathf.Clamp(directionX, -1, 1),
            Mathf.Clamp(directionY, -1, 1));
    }

    protected virtual void Move(float moveSpeedX, float moveSpeedY)
    {
        OnMove?.Invoke(moveSpeedX, moveSpeedY);
    }

    public abstract void DisableMove();

}
