using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public event Action OnAttackStart;
    public event Action<string> OnAttackAnimationHash;
    public event Action<int> OnAttack;
    public event Action OnAttackEnd;

    public event Action OnResetStates;
    public event Action<bool> OnBlock;

    public virtual void Attack()
    {
        OnAttackStart?.Invoke();
    }

    public virtual void AttackEnd()
    {
        OnAttackEnd?.Invoke();
    }

    public virtual void Block(bool blockState)
    {
        OnBlock?.Invoke(blockState);
    }

    protected void InvokeOnAttackAnimationHash(string hash)
    {
        OnAttackAnimationHash?.Invoke(hash);
    }

    protected virtual void ResetStates()
    {
        OnResetStates?.Invoke();
    }
}