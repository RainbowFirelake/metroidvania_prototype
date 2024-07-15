using Metroidvania.AllyAndEnemy;
using System;
using UnityEngine;

namespace Metroidvania.AI
{
    public class DetectionArea : MonoBehaviour
    {
        public event Action<AllyAndEnemySystem> OnCharacterEnter;
        public event Action<AllyAndEnemySystem> OnCharacterExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var side = other.GetComponent<AllyAndEnemySystem>();
            if (side)
            {
                OnCharacterEnter?.Invoke(side);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var side = other.GetComponent<AllyAndEnemySystem>();
            if (side)
            {
                OnCharacterExit?.Invoke(side);
            }
        }
    }
}
