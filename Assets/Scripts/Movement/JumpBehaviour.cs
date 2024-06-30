using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpBehaviour : MonoBehaviour
{
    public bool IsGrounded { get; private set; } = true;

    public event Action OnJumpStart;

    private float _jumpBufferCounter;
    private float _coyoteTimeCounter;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private MoveParams _moveParams;
    [SerializeField]
    private GroundDetector _groundDetector;

    private void OnValidate()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _groundDetector.OnGroundEnter += SetGrounded;
        _groundDetector.OnGroundExit += UnsetGrounded;
    }

    private void OnDisable()
    {
        _groundDetector.OnGroundEnter -= SetGrounded;
        _groundDetector.OnGroundExit -= UnsetGrounded;
    }

    public void Update() => UpdateParameters();

    public void Jump(Vector2 movementDirection, KeyState state)
    {
        if (state == KeyState.Default && _jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            PerformJump(movementDirection);
            return;
        }

        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
        {
            KeepVerticalMove();
        }

        if (_rigidbody2D.velocity.y > 0 && state == KeyState.KeyUp)
        {
            FallDown();
        }

        OnJumpStart?.Invoke();
    }

    private void FallDown()
    {
        _rigidbody2D.velocity = new Vector2(
                        _rigidbody2D.velocity.x,
                        _rigidbody2D.velocity.y * 0.5f);

        _coyoteTimeCounter = 0f;
    }

    private void KeepVerticalMove()
    {
        _rigidbody2D.velocity = new Vector2(
                        _rigidbody2D.velocity.x, _moveParams.JumpStrength);

        _jumpBufferCounter = 0f;
    }

    private void PerformJump(Vector2 movementDirection)
    {
        Vector2 jumpDirection = new Vector2(
                        _rigidbody2D.velocity.x * movementDirection.x,
                        _moveParams.JumpStrength);

        _rigidbody2D.AddForce(jumpDirection, ForceMode2D.Impulse);

        return;
    }

    private void UpdateParameters()
    {
        if (IsGrounded)
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

    private void SetGrounded() => IsGrounded = true;

    private void UnsetGrounded() => IsGrounded = false;
}
