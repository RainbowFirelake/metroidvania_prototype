using Metroidvania.Movement;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private RigidbodyMovementBehaviour _movement;
    [SerializeField] private Transform _attackPoint;

    private Direction _currentDirection = Direction.Right;

    void OnEnable()
    {
        _movement.OnMove += FlipX;
    }

    private void FlipX(Vector2 speed)
    {
        if (speed.x <= -0.01f)
        {
            _spriteRenderer.flipX = true;
            FlipAttackPoint(Direction.Left);
        }
        else if (speed.x >= 0.01f)
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
