using Metroidvania.Combat;
using Metroidvania.Movement;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BasicMovementBehaviour _movement;
    [SerializeField] private AttackerBehaviour _attacker;
    [SerializeField] private DashBehaviour _dasher;

    private void OnEnable()
    {
        _movement.OnMove += SetMoveState;

        _dasher.OnStartDash += SetDashState;
        _dasher.OnEndDash += UnsetDashState;

        _attacker.OnAttackStart += SetAttackStart;
        _attacker.OnAttackEnd += SetAttackEnd;
    }

    private void OnDisable()
    {
        _movement.OnMove -= SetMoveState;

        _dasher.OnStartDash -= SetDashState;
        _dasher.OnEndDash -= UnsetDashState;

        _attacker.OnAttackStart -= SetAttackStart;
        _attacker.OnAttackEnd += SetAttackEnd;
    }

    private void SetMoveState(Vector2 speed)
    {
        _animator.SetFloat("horizontalSpeed", Mathf.Abs(speed.x));
        _animator.SetFloat("verticalSpeed", speed.y);
    }

    private void SetInAirState(bool isInAir)
    {
        _animator.SetBool("InAir", isInAir);
    }

    private void SetAttackStart(int animationHash)
    {
        _animator.Play(animationHash, 0);
        _animator.SetBool("InAttack", true);
    }

    private void SetAttackEnd()
    {
        _animator.SetBool("InAttack", false);
    }

    private void SetAttackState(int comboCount)
    {
        _animator.SetInteger("comboCount", comboCount);
    }

    private void SetBlockState(bool state)
    {
        _animator.SetTrigger("Block");
    }

    private void SetDashState()
    {
        Debug.Log("Set Dash State");
        _animator.SetBool("dash", true);
    }

    private void UnsetDashState()
    {
        Debug.Log("Unset Dash State");
        _animator.SetBool("dash", false);
    }
}
