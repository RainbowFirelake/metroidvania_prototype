using Unity.VisualScripting;
using UnityEngine;

public class Combat : BasicCombat
{
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private float _timeToContinueAttack = 2f;
    [SerializeField] private int _maxComboAttacks = 3;

    [SerializeField] private Stats _stats;
    [SerializeField] private AllyAndEnemySystem _allyAndEnemy;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _damageLayer;

    private float _timeAfterLastAttack = Mathf.Infinity;
    private bool _canAttack = true;

    void Start()
    {
        _canAttack = true;
    }

    private void Update()
    {      
        if (_timeAfterLastAttack >= _timeToContinueAttack 
            || _currentComboAttack >= _maxComboAttacks)
        {
            _timeAfterLastAttack = 0;
            _canAttack = true;
            base.ResetStates();
        }

        UpdateTimers();
    }

    public override void AttackStart()
    {
        if (_canAttack)
        {
            base.AttackStart();
            _canAttack = false;
        }
    }

    public override void Attack()
    {
        base.Attack();

        AttackStart();
    }

    public override void Hit()
    {
        var attackInfo = _currentWeapon.AttacksInfo[_currentComboAttack];
        Collider2D[] hittedColliders = Physics2D.OverlapCircleAll(
            _attackPoint.position, 
            attackInfo.AttackRange,
            _damageLayer);

        foreach (var collider in hittedColliders)
        {
            if (DealDamage(collider, attackInfo) 
            && !_currentWeapon.CanHitMultipleEnemies)
                return;
        }
    }

    // NEEDS TO REWRITE
    // because dealdamage method must only deal damage
    // not to compare sides
    private bool DealDamage(Collider2D enemy, WeaponAttackInfo attackInfo)
    {   
        var side = enemy.GetComponent<AllyAndEnemySystem>();
        var enemyHealth = enemy.GetComponent<HealthSystem>();
        var modifiable = enemy.GetComponent<ModifiableActor>();
        if (enemyHealth && side && side.characterSide != _allyAndEnemy.characterSide)
        {
            enemyHealth.TakeDamage(attackInfo.Damage);
            if (modifiable)
            {
                modifiable.AddModifier(_currentWeapon.Modifiers[0]);
            }
            return true;
        }
        return false;
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
        _canAttack = true;
        _timeAfterLastAttack = 0;
    }

    public override void Block(bool blockState)
    {
        base.Block(blockState);
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint == null) 
        {
            return;
        }


        // Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    protected override void ResetStates()
    {
        base.ResetStates();

        _canAttack = true;
    }

    private void UpdateTimers()
    {
        _timeAfterLastAttack += Time.deltaTime;
    }
}