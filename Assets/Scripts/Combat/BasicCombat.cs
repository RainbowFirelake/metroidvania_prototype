using UnityEngine;
using System;

public abstract class BasicCombat : MonoBehaviour
{
    public event Action OnAttackStart;
    public event Action<int> OnAttack;
    public event Action OnAttackEnd;

    public event Action OnResetStates;
    public event Action<bool> OnBlock;

    protected int _currentComboAttack = 0;

    public virtual void AttackStart()
    {
        OnAttackStart?.Invoke();
    }

    public virtual void Attack()
    {
        OnAttack?.Invoke(_currentComboAttack);
    }
    
    public abstract void Hit();

    public virtual void IncreaseComboCount()
    {
        _currentComboAttack++;
    }

    public virtual void AttackEnd()
    {
        OnAttackEnd?.Invoke();
    }

    public virtual void Block(bool blockState)
    {
        OnBlock?.Invoke(blockState);
    }

    protected virtual void ResetStates()
    {
        _currentComboAttack = 0;
        OnResetStates?.Invoke();
    }
}