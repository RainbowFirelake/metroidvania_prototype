using UnityEngine;

namespace Metroidvania.AllyAndEnemy
{
    public class AllyAndEnemySystem : MonoBehaviour
    {
        [SerializeField] private CharacterSide _characterSide;
        public CharacterSide CharacterSide { get { return _characterSide; } }
    }
}
