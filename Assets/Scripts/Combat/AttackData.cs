using UnityEngine;

namespace Metroidvania.Combat
{
    [CreateAssetMenu(menuName = "Weapons/AttackData")]
    public class AttackData : ScriptableObject
    {
        [field: SerializeField]
        public AnimationClip AttackAnimation { get; private set; }

        [field: SerializeField]
        public int AnimationHash { get; private set; }

        [field: SerializeField]
        public float DamagePercentModifier { get; private set; } = 1f;

        [field: SerializeField]
        public float AttackRange { get; private set; } = 1f;

        private void OnValidate()
        {
            AnimationHash = Animator.StringToHash(AttackAnimation.name);
        }
    }
}
