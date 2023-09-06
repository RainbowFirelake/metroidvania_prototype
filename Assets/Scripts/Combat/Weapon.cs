using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class Weapon : ScriptableObject
{
    [field : SerializeField]
    public List<WeaponAttackInfo> AttacksInfo { get; private set; } = null;
    [field : SerializeField]
    public bool CanHitMultipleEnemies { get; private set; } = true;
    [field : SerializeField]
    public List<BaseModifier> Modifiers { get; private set; } = null;
}