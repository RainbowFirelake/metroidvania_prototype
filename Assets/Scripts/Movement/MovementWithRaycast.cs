using UnityEngine;

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
    }
}

