using System;
using System.Collections;
using UnityEngine;

namespace Metroidvania.Movement
{
    public class RigidbodyMovementBehaviour : BasicMovementBehaviour
    {
        public Action MovementCallback;

        [SerializeField] private MoveParams _moveParams;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private bool IsMovementInterrupted = false;
        private float _savedGravityScale;

        private void Start()
        {
            _savedGravityScale = _rigidbody2D.gravityScale;
        }

        protected override IEnumerator InterruptMovement(float seconds)
        {
            _rigidbody2D.velocity = Vector2.zero;
            InvokeOnMove(Vector2.zero);

            _rigidbody2D.gravityScale = 0;
            IsMovementInterrupted = true;

            yield return new WaitForSeconds(seconds);

            IsMovementInterrupted = false;
            _rigidbody2D.gravityScale = _savedGravityScale;
        }

        protected override void Move()
        {
            if (IsMovementInterrupted)
            {
                return;
            }

            var vector = new Vector2(
                _moveParams.MoveSpeed * MovementDirection.x,
                _rigidbody2D.velocity.y);

            InvokeOnMove(vector);

            _rigidbody2D.velocity = vector;
        }

        public override void ResetMovementState()
        {
            StopCoroutine(InterruptMovement(0));
            _rigidbody2D.gravityScale = _savedGravityScale;
            IsMovementInterrupted = false;
        }
    }
}
