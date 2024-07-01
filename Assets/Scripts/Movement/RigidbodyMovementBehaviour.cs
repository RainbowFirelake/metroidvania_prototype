using System;
using UnityEngine;

namespace Metroidvania.Movement
{
    public class RigidbodyMovementBehaviour : BasicMovementBehaviour
    {
        public Action MovementCallback;

        [SerializeField] private MoveParams _moveParams;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private WallDetector _leftDetector;
        [SerializeField] private WallDetector _rightDetector;

        private void FixedUpdate()
        {
            Move();
        }

        protected void Move()
        {
            base.Move(MovementDirection.x, _rigidbody2D.velocity.y);

            var vector = new Vector2(
                _moveParams.MoveSpeed * MovementDirection.x,
                _rigidbody2D.velocity.y);

            _rigidbody2D.velocity = vector;
        }
    }
}