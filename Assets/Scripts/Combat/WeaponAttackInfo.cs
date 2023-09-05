using UnityEngine;


[System.Serializable]
public class WeaponAttackInfo
{
    [field : SerializeField] 
    public float Damage { get; private set; }
    [field : SerializeField]
    public float AttackRange { get; private set; }
}