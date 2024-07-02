using System;
using UnityEngine;

namespace Metroidvania.Movement
{
    public class RigidbodyMovementBehaviour : BasicMovementBehaviour
    {
        public Action MovementCallback;

        [SerializeField] private MoveParams _moveParams;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        protected override void Move()
        {
            var vector = new Vector2(
                _moveParams.MoveSpeed * MovementDirection.x,
                _rigidbody2D.velocity.y);

            InvokeOnMove(vector);

            _rigidbody2D.velocity = vector;
        }
    }
}
