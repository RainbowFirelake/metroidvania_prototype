using UnityEngine;

public class MovementController : MonoBehaviour, IControllable
{
    [SerializeField]
    private BasicMovementBehaviour _movement;
    [SerializeField]
    private DashBehaviour _dashBehaviour;
    [SerializeField]
    private JumpBehaviour _jumpBehaviour;

    [SerializeField] private BasicCombat _combat;

    private bool _canMove = true;

    private void OnEnable()
    {
        _combat.OnAttackStart += DisableMovement;
        _combat.OnAttackEnd += EnableMovement;
    }

    public void MoveControllable(float horizontalInput, float verticalInput)
    {
        if (!_canMove)
        {
            StopMove();
            return;
        }

        _movement.SetDirection(horizontalInput, verticalInput);
    }

    public void Attack()
    {
        if (!_jumpBehaviour.IsGrounded || _dashBehaviour.IsDashing) return;

        _combat.Attack();
    }

    public void Block()
    {
        _combat.Block(true);
    }

    public void Dash()
    {
        _dashBehaviour.DashIfCan(_movement.MovementDirection.x);
    }

    public void Jump(KeyState state = KeyState.Default)
    {
        _jumpBehaviour.Jump(_movement.MovementDirection, state);
    }

    private void StopMove()
    {
        _movement.DisableMove();
    }

    private void EnableMovement()
    {
        _canMove = true;
    }

    private void DisableMovement()
    {
        _canMove = false;
        StopMove();
    }
}
