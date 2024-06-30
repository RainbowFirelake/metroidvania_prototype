using UnityEngine;

namespace Metroidvania.Movement
{
    public class MovementWithColliders : BasicMovementBehaviour
    {
        [SerializeField] private MoveParams _moveParams;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private WallDetector _leftDetector;
        [SerializeField] private WallDetector _rightDetector;

        private float _coyoteTimeCounter = Mathf.Infinity;
        private float _jumpBufferCounter;

        private Transform _transform;

        private float _lastMoveDirectionX;

        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            Move();

            if (MovementDirection.x != 0)
            {
                _lastMoveDirectionX = MovementDirection.x;
            }
        }

        protected void Move()
        {
            var vector = new Vector2(
                _moveParams.MoveSpeed * MovementDirection.x,
                _rigidbody2D.velocity.y);

            _rigidbody2D.velocity = vector;


            base.Move(MovementDirection.x, _rigidbody2D.velocity.y);
        }

        //protected override void JumpBehaviour(KeyState state)
        //{
        //    if (state == KeyState.Default && _jumpBufferCounter > 0f && 
        //        _coyoteTimeCounter > 0f) 
        //    {
        //        Vector2 jumpDirection = new Vector2(
        //        _rigidbody2D.velocity.x * moveDirectionX,
        //        _moveParams.JumpStrength);
        //        _rigidbody2D.AddForce(jumpDirection, ForceMode2D.Impulse);

        //        return;
        //    }

        //    if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
        //    {
        //        _rigidbody2D.velocity = new Vector2(
        //            _rigidbody2D.velocity.x, _moveParams.JumpStrength);

        //        _jumpBufferCounter = 0f;
        //    }

        //    if (_rigidbody2D.velocity.y > 0 && state == KeyState.KeyUp)
        //    {
        //        _rigidbody2D.velocity = new Vector2(
        //            _rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);

        //        _coyoteTimeCounter = 0f;
        //    }
        //}

        //protected override void DashBehaviour()
        //{
        //    if (_canDash)
        //    {
        //        StartCoroutine(DashCoroutine());
        //    }
        //}

        //public IEnumerator DashCoroutine()
        //{
        //    _canDash = false;
        //    _isDashing = true;
        //    float originalGravity = _rigidbody2D.gravityScale;
        //    _rigidbody2D.gravityScale = 0f;
        //    _rigidbody2D.velocity = new Vector2(
        //        _lastMoveDirectionX *
        //        _moveParams.DashStrength, 0f);

        //    yield return new WaitForSeconds(_moveParams.DashTime);
        //    _rigidbody2D.gravityScale = originalGravity;
        //    _isDashing = false;
        //    yield return new WaitForSeconds(_moveParams.DashCooldown);
        //    _canDash = true;
        //}

        public override void DisableMove()
        {

        }
    }
}
