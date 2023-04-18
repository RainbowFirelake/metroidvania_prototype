using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BasicMovement _movement;
    [SerializeField] private BasicCombat _combat;

    private void OnEnable()
    {
        _movement.OnMove += SetMoveState;  
        _movement.OnDash += SetDashState;
        
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
        _animator.SetTrigger("dash");
    }

    private void ResetCombatStates()
    {
        _animator.SetInteger("comboCount", 0);
        _animator.ResetTrigger("startAttack");
    }
}
