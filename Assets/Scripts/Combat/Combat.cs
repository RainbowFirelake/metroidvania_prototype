using UnityEngine;

public class Combat : BasicCombat
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _timeToContinueAttack = 2f;
    [SerializeField] private int _maxComboAttacks = 3;

    [SerializeField] private Stats _stats;
    [SerializeField] private AllyAndEnemySystem _allyAndEnemy;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;

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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            _attackPoint.position, 
            _attackRange, 
            _enemyLayer);
        foreach(var enemy in hitEnemies)
        {
            var side = enemy.GetComponent<AllyAndEnemySystem>();
            var enemyHealth = enemy.GetComponent<HealthSystem>();
            if (enemyHealth && side && 
            side.characterSide != _allyAndEnemy.characterSide)
            {
                enemyHealth.TakeDamage(_stats.GetStatValue(StatType.AttackDamage));
            }
        }
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

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
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