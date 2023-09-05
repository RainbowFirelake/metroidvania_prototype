using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithColliders : BasicMovement
{
    [SerializeField] private MoveParams _moveParams;
    [SerializeField] private GroundDetector _groundDetector;
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

    private void OnEnable()
    {
        _groundDetector.OnGroundEnter += EnableGrounded;
        _groundDetector.OnGroundExit += DisableGrounded;
    }

    private void OnDisable()
    {
        _groundDetector.OnGroundEnter -= EnableGrounded;
        _groundDetector.OnGroundExit -= DisableGrounded;
    }

    private void Update()
    {
        UpdateParameters();
    }

    private void FixedUpdate()
    {
        Move();
        if (moveDirectionX != 0)
        {
            _lastMoveDirectionX = moveDirectionX;
        }
    }

    protected void Move()
    {
        if (_isDashing) return;

        var vector = new Vector2(
            _moveParams.MoveSpeed * moveDirectionX,
            _rigidbody2D.velocity.y);

        if (_moveParams.IsMovementActiveWhenInAir && !_isGrounded)
        {
            vector = new Vector2(
                _moveParams.MoveSpeedInAir * moveDirectionX,
                _rigidbody2D.velocity.y);
        }

        if (!_moveParams.IsMovementActiveWhenInAir && !_isGrounded)
        {
            vector = _rigidbody2D.velocity;
        }

        _rigidbody2D.velocity = vector;


        base.Move(_rigidbody2D.velocity.x + moveDirectionX, _rigidbody2D.velocity.y);
    }

    protected override void JumpBehaviour(KeyState state)
    {
        if (state == KeyState.Default && _jumpBufferCounter > 0f && 
            _coyoteTimeCounter > 0f) 
        {
            Vector2 jumpDirection = new Vector2(
            _rigidbody2D.velocity.x * moveDirectionX,
            _moveParams.JumpStrength);
            _rigidbody2D.AddForce(jumpDirection, ForceMode2D.Impulse);

            return;
        }

        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, _moveParams.JumpStrength);
            
            _jumpBufferCounter = 0f;
        }

        if (_rigidbody2D.velocity.y > 0 && state == KeyState.KeyUp)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
            
            _coyoteTimeCounter = 0f;
        }
    }

    protected override void DashBehaviour()
    {
        if (_canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    public IEnumerator DashCoroutine()
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(
            _lastMoveDirectionX *
            _moveParams.DashStrength, 0f);
            
        yield return new WaitForSeconds(_moveParams.DashTime);
        _rigidbody2D.gravityScale = originalGravity;
        _isDashing = false;
        yield return new WaitForSeconds(_moveParams.DashCooldown);
        _canDash = true;
    }

    public override void DisableMove()
    {
        moveDirectionX = 0;
        moveDirectionY = 0;
    }

    private void UpdateParameters()
    {
        if (_isGrounded)
        {
            _coyoteTimeCounter = _moveParams.CoyoteTime;
            _jumpBufferCounter = _moveParams.JumpBufferingTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void EnableGrounded()
    {
        _isGrounded = true;
    }

    private void DisableGrounded()
    {
        _isGrounded = false;
    }

    public override void EnableMove()
    {
        
    }
}
