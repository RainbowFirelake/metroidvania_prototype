using Metroidvania.AllyAndEnemy;
using System;
using System.Collections;
using UnityEngine;

namespace Metroidvania.Combat
{
    public class AttackerBehaviour : MonoBehaviour
    {
        public event Action<int> OnAttackStart;
        public event Action OnAttackEnd;

        [field: SerializeField]
        public bool CanAttack { get; private set; } = true;
        public int CurrentMaxComboAttacks
        {
            get => _currentWeapon != null
                    ? _currentWeapon.AttacksInfo.Count
                    : -1;
        }

        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private float _timeToContinueAttack = 2f;

        [SerializeField] private Stats _stats;
        [SerializeField] private AllyAndEnemySystem _allyAndEnemy;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _damageLayer;

        private float _timeAfterLastAttack = Mathf.Infinity;
        private int _currentCombo = 0;

        private bool _isComboTimerActive = false;

        void Start()
        {
            CanAttack = true;
        }

        private void Update()
        {
            if (_timeAfterLastAttack >= _timeToContinueAttack
                || _currentCombo >= CurrentMaxComboAttacks)
            {
                _timeAfterLastAttack = 0;
                CanAttack = true;
                ResetStates();
            }

            UpdateTimers();
        }

        public void InterruptAttack()
        {
            if (!CanAttack)
            {
                CanAttack = true;
                _timeAfterLastAttack = 0;
                _isComboTimerActive = true;
                OnAttackEnd?.Invoke();
            }
        }

        public void Attack()
        {
            if (!CanAttack)
            {
                return;
            }

            OnAttackStart?.Invoke(_currentWeapon.AttacksInfo[_currentCombo].AnimationHash);

            _isComboTimerActive = false;

            CanAttack = false;

            StartCoroutine(UnblockAttackState(_currentWeapon.AttacksInfo[_currentCombo].AttackAnimation.length));
        }

        public void Hit()
        {
            var attackInfo = _currentWeapon.AttacksInfo[_currentCombo];

            Collider2D[] hittedColliders = Physics2D.OverlapCircleAll(
                _attackPoint.position,
                attackInfo.AttackRange,
                _damageLayer);

            foreach (var collider in hittedColliders)
            {
                if (DealDamage(collider, attackInfo) && !_currentWeapon.CanHitMultipleEnemies)
                    return;
            }
        }

        public float GetNextAttackAnimationDurationInSeconds()
        {
            return _currentCombo + 1 < CurrentMaxComboAttacks
                ? _currentWeapon.AttacksInfo[_currentCombo + 1].GetAnimationDurationInSeconds()
                : 0;
        }

        private bool DealDamage(Collider2D enemy, AttackData attackInfo)
        {
            var side = enemy.GetComponent<AllyAndEnemySystem>();
            var enemyHealth = enemy.GetComponent<HealthSystem>();
            var modifiable = enemy.GetComponent<ModifiableActor>();

            if (enemyHealth && side && side.CharacterSide != _allyAndEnemy.CharacterSide)
            {
                enemyHealth.TakeDamage(_currentWeapon.Damage * attackInfo.DamagePercentModifier);

                if (modifiable)
                {
                    modifiable.AddModifier(_currentWeapon.Modifiers[0]);
                }

                return true;
            }

            return false;
        }

        public IEnumerator UnblockAttackState(float durationInSeconds)
        {
            yield return new WaitForSeconds(durationInSeconds);
            AttackEnd();
        }

        public void AttackEnd()
        {
            CanAttack = true;
            _timeAfterLastAttack = 0;
            _currentCombo++;
            _isComboTimerActive = true;
            OnAttackEnd?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (_attackPoint == null)
            {
                return;
            }

            // Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }

        protected void ResetStates()
        {
            _currentCombo = 0;
            CanAttack = true;
        }

        private void UpdateTimers()
        {
            if (_isComboTimerActive)
            {
                _timeAfterLastAttack += Time.deltaTime;
            }
        }
    }
}
