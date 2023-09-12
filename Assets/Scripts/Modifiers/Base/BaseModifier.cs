using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseModifier : ScriptableObject
{
    public event Action<int> OnEffectEnd;

    

    [field : SerializeField]
    public ModifierType Type { get; private set; }
    
    [field : SerializeField]
    public GameObject VFX { get; protected set; }

    [Range(1, 20)] [SerializeField]
    protected int _effectTimes = 1;
    public int EffectTimes { get { return _effectTimes; } }

    public abstract void BeginEffect(ModifiableActor target);
    public abstract void GiveEffect(ModifiableActor target);
    public void CreateVFX(Vector3 position, Quaternion rotation)
    {
        if (VFX == null) return;
        Instantiate(VFX, position, rotation);
    }

    public abstract void EndEffect();
}
