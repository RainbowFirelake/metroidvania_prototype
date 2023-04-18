using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithColliders : BasicMovement
{
    [SerializeField] private MoveParams _moveParams;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Climber _climber;

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
        UpdateTimers();
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

        base.Move(moveDirectionX, _rigidbody2D.velocity.y);
    }

    public override void Jump()
    {
        if (_jumpBufferCounter > 0f && 
        _coyoteTimeCounter > 0f) 
        {
            Vector2 jumpDirection = new Vector2(
            _rigidbody2D.velocity.x * moveDirectionX,
            _moveParams.JumpStrength);
            _rigidbody2D.AddForce(jumpDirection, ForceMode2D.Impulse);
            base.Jump();
        }
    }

    public override void Dash()
    {
        if (_canDash)
        {
            base.Dash();
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

    private void UpdateTimers()
    {
        if (_isGrounded)
        {
            _coyoteTimeCounter = _moveParams.CoyoteTime;
            _jumpBufferCounter = _moveParams.JumpBufferingTime;
        }

        _coyoteTimeCounter -= Time.deltaTime;
        _jumpBufferCounter -= Time.deltaTime;
    }

    private void EnableGrounded()
    {
        _isGrounded = true;
    }

    private void DisableGrounded()
    {
        _isGrounded = false;
    }
}
