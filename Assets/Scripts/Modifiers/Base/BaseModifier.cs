using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModifier : ScriptableObject
{
    [field : SerializeField]
    public GameObject ModifierVFX { get; protected set; }

    [Range(1, 20)] [SerializeField]
    protected int EffectTimes = 1;
    protected int _currentEffectTimes = 0;

    protected ModifiableActor _target;

    public abstract void BeginEffect(ModifiableActor target);

    public abstract IEnumerator GiveEffect(ModifiableActor target);

    public abstract void EndEffect();
}
