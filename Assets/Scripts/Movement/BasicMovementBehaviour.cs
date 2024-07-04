using System;
using System.Collections;
using UnityEngine;

namespace Metroidvania.Movement
{
    public abstract class BasicMovementBehaviour : MonoBehaviour, IFixedUpdate
    {
        public event Action<Vector2> OnMove;

        public Vector2 MovementDirection { get; private set; } = Vector2.zero;

        public virtual void SetDirection(float directionX, float directionY)
        {
            MovementDirection = new Vector2(
                Mathf.Clamp(directionX, -1, 1),
                Mathf.Clamp(directionY, -1, 1));
        }

        public void Stop()
        {
            MovementDirection = new Vector2();
        }

        protected abstract void Move();

        public void HandleFixedUpdate()
        {
            Move();
        }

        public void InterruptMovementDuringGivenSeconds(float seconds)
        {
            ResetMovementState();
            StartCoroutine(InterruptMovement(seconds));
        }

        public abstract void ResetMovementState();

        protected abstract IEnumerator InterruptMovement(float seconds);

        public void InvokeOnMove(Vector2 speedVector)
        {
            OnMove?.Invoke(speedVector);
        }
    }
}
