using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BasicMovementBehaviour _movement;
    [SerializeField] private BasicCombat _combat;
    [SerializeField] private DashBehaviour _dashBehaviour;

    private void OnEnable()
    {
        _movement.OnMove += SetMoveState;
        _dashBehaviour.OnStartDash += SetDashState;
        _dashBehaviour.OnEndDash += UnsetDashState;

        _combat.OnAttackStart += SetAttackStart;
        _combat.OnAttack += SetAttackState;
        _combat.OnAttackEnd += SetAttackEnd;
        _combat.OnBlock += SetBlockState;
        _combat.OnResetStates += ResetCombatStates;
    }

    private void OnDisable()
    {
        _movement.OnMove -= SetMoveState;
        _combat.OnAttack -= SetAttackState;
        _combat.OnBlock -= SetBlockState;
        _combat.OnResetStates -= ResetCombatStates;
    }

    private void SetMoveState(float horizontalSpeed, float verticalSpeed)
    {
        _animator.SetFloat("horizontalSpeed", Mathf.Abs(horizontalSpeed));
        _animator.SetFloat("verticalSpeed", verticalSpeed);
    }

    private void SetInAirState(bool isInAir)
    {
        _animator.SetBool("InAir", isInAir);
    }

    private void SetAttackStart()
    {
        _animator.SetTrigger("startAttack");
    }

    private void SetAttackState(int comboCount)
    {
        _animator.SetInteger("comboCount", comboCount);
    }

    private void SetAttackEnd()
    {
        _animator.SetTrigger("endAttack");
    }

    private void SetBlockState(bool state)
    {
        _animator.SetTrigger("Block");
    }

    private void SetDashState()
    {
        _animator.SetBool("dash", true);
    }

    private void UnsetDashState()
    {
        _animator.SetBool("dash", false);
    }

    private void ResetCombatStates()
    {
        _animator.SetInteger("comboCount", 0);
        _animator.ResetTrigger("startAttack");
    }
}
