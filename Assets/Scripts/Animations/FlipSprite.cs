using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BasicMovementBehaviour _movement;
    [SerializeField] private Transform _attackPoint;

    private Direction _currentDirection = Direction.Right;

    void OnEnable()
    {
        _movement.OnMove += FlipX;
    }

    private void FlipX(float horizontalSpeed, float verticalSpeed)
    {
        if (horizontalSpeed <= -0.01f)
        {
            _spriteRenderer.flipX = true;
            FlipAttackPoint(Direction.Left);
        }
        else if (horizontalSpeed >= 0.01f)
        {
            _spriteRenderer.flipX = false;
            FlipAttackPoint(Direction.Right);
        }
    }

    private void FlipAttackPoint(Direction direction)
    {
        if (_attackPoint == null || direction == _currentDirection) return;

        _attackPoint.localPosition = new Vector3(
            -_attackPoint.localPosition.x,
            _attackPoint.localPosition.y,
            _attackPoint.localPosition.z);
        _currentDirection = direction;
    }

    enum Direction
    {
        Left,
        Right
    }
}
