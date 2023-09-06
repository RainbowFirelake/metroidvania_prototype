using System.Collections.Generic;
using UnityEngine;

public class ModifiableActor : MonoBehaviour
{
    private List<BaseModifier> _modifiers = null;

    void Start()
    {
        _modifiers = new List<BaseModifier>();
    }

    public void AddModifier(BaseModifier modifier)
    {
        _modifiers.Add(modifier);
        StartCoroutine(modifier.GiveEffect(this));
    }
}