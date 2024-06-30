using UnityEngine;
using UnityUtils;

namespace Metroidvania.Movement
{
    public class MovementWithRaycast : BasicMovementBehaviour
    {
        [SerializeField] private MoveParams _moveParams;

        private float _raycastWallLength = 0.7f;
        private float _raycastGroundLength = 1f;
        private float _lastMoveDirection;
        private bool _isClimbing = false;
        private float _timeAfterExitingGround = Mathf.Infinity;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _raycastGroundLength = _transform.localScale.y / 2 + 0.1f;
            _raycastWallLength = _transform.localScale.x / 2 + 0.1f;
        }

        void Update()
        {
            UpdateLastMoveDirection();
            //DetectWall(_lastMoveDirection);
            //DetectGround();
            Move(MovementDirection.x);

            if (_moveParams.CanClimbOnWalls) Climb(MovementDirection.y);

            _timeAfterExitingGround += Time.deltaTime;
        }

        private void UpdateLastMoveDirection()
        {
            if (MovementDirection.x != 0)
            {
                _lastMoveDirection = MovementDirection.x;
            }
        }

        public void Move(float direction)
        {
            Vector2 velocity = new Vector2();
            if (_moveParams.CanClimbOnWalls && _isClimbing)
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                return;
            }

            //if (IsGrounded)
            //{
            //    velocity = new Vector2(
            //        _moveParams.MoveSpeed * direction, // x
            //        _rigidbody2D.velocity.y // y
            //    );
            //}
            //else
            //{
            //    velocity = new Vector2(
            //        _moveParams.MoveSpeedInAir * direction, // x
            //        _rigidbody2D.velocity.y // y
            //    );
            //}

            _rigidbody2D.velocity = velocity;
            //Debug.Log("x speed: " + _rigidbody2D.velocity.x + "y speed: " + _rigidbody2D.velocity.y);
        }

        public void Climb(float direction)
        {
            if (!_isClimbing) return;
            Vector2 velocity = new Vector2();
            if (_isClimbing)
            {
                velocity = new Vector2(
                    _rigidbody2D.velocity.x, // x
                    _moveParams.ClimbSpeed * direction   // y
                );
            }

            _rigidbody2D.velocity = velocity;
        }

        public RaycastHit2D RaycastDetection(Vector2 direction, int layerMask, float length)
        {
            return Physics2D.Raycast(
                _transform.position,
                direction,
                length,
                layerMask
            );
        }

        //public bool DetectWall(float horizontalDirection)
        //{
        //    var layerMask = LayerMask.GetMask("Ground");
        //    Vector2 horizontal = Vector2.right * horizontalDirection;
        //    var hit1 = Physics2D.Raycast(
        //        _transform.position,
        //        horizontal,
        //        _raycastWallLength,
        //        layerMask
        //    );

        //    var hit2 = Physics2D.Raycast(
        //        _transform.position - new Vector3(0, _transform.localScale.y / 2),
        //        horizontal,
        //        _raycastWallLength,
        //        layerMask
        //    );

        //    var hit3 = Physics2D.Raycast(
        //        _transform.position + new Vector3(0, _transform.localScale.y / 2),
        //        horizontal,
        //        _raycastWallLength,
        //        layerMask
        //    );

        //    if (hit1 || hit2 || hit3)
        //    {
        //        _isClimbing = true;
        //        return true;
        //    }

        //    _isClimbing = false;
        //    return false;
        //}

        //public void DetectGround()
        //{
        //    var hit = RaycastDetection(
        //        Vector2.down,
        //        LayerMask.GetMask("Ground"),
        //        _raycastGroundLength
        //    );

        //    if (hit)
        //    {
        //        _timeAfterExitingGround = 0;
        //        IsGrounded = true;
        //        return;
        //    }
        //    IsGrounded = false;
        //}

        //protected override void JumpBehaviour(KeyState state = KeyState.Default)
        //{
        //    if (!_isGrounded && _timeAfterExitingGround > _moveParams.CoyoteTime) return;

        //    Vector2 jumpDirection = new Vector2(
        //        _rigidbody2D.velocity.x * moveDirectionX,
        //        _moveParams.JumpStrength
        //    );
        //    _rigidbody2D.AddForce(jumpDirection, ForceMode2D.Impulse);
        //}

        //protected override void DashBehaviour()
        //{
        //    if (_isGrounded)
        //    {
        //        Vector2 dashDirection = new Vector2(_moveParams.DashStrength * _lastMoveDirection, 0);
        //        _rigidbody2D.AddForce(dashDirection, ForceMode2D.Impulse);
        //    }
        //}

        public override void DisableMove()
        {
            MovementDirection.With(0, 0);
        }
    }
}

