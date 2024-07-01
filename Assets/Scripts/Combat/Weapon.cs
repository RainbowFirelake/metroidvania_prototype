using System.Collections.Generic;
using UnityEngine;

namespace Metroidvania.Combat
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
    public class Weapon : ScriptableObject
    {
        [field: SerializeField]
        public List<AttackData> AttacksInfo { get; private set; } = null;
        [field: SerializeField]
        public float Damage = 10f;
        [field: SerializeField]
        public bool CanHitMultipleEnemies { get; private set; } = true;
        [field: SerializeField]
        public List<BaseModifier> Modifiers { get; private set; } = null;
    }
}
