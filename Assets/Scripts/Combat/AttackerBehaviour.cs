using System;
using UnityEngine;

namespace Metroidvania.Combat
{
    public class AttackerBehaviour : MonoBehaviour
    {
        public event Action<int> OnAttackStart;
        public event Action OnAttackEnd;

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
        private bool _canAttack = true;
        private int _currentCombo = 0;

        private bool _isComboTimerActive = false;

        void Start()
        {
            _canAttack = true;
        }

        private void Update()
        {
            if (_timeAfterLastAttack >= _timeToContinueAttack
                || _currentCombo >= CurrentMaxComboAttacks)
            {
                _timeAfterLastAttack = 0;
                _canAttack = true;
                OnAttackEnd?.Invoke();
                ResetStates();
            }

            UpdateTimers();
        }

        public void Attack()
        {
            if (!_canAttack)
            {
                return;
            }

            OnAttackStart?.Invoke(_currentWeapon.AttacksInfo[_currentCombo].AnimationHash);

            _isComboTimerActive = false;

            _canAttack = false;
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

        private bool DealDamage(Collider2D enemy, AttackData attackInfo)
        {
            var side = enemy.GetComponent<AllyAndEnemySystem>();
            var enemyHealth = enemy.GetComponent<HealthSystem>();
            var modifiable = enemy.GetComponent<ModifiableActor>();

            if (enemyHealth && side && side.characterSide != _allyAndEnemy.characterSide)
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

        public void AttackEnd()
        {
            _canAttack = true;
            _timeAfterLastAttack = 0;
            _currentCombo++;
            _isComboTimerActive = true;
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
            _canAttack = true;
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
