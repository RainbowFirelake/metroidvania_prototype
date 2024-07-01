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
    }

    private void OnDisable()
    {
        _movement.OnMove -= SetMoveState;
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

    private void SetAttackStart(int animationHash)
    {
        _animator.Play(animationHash, 0);
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
