using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new FireModifier", menuName = "Modifiers/New FireModifier")]
public class FireModifier : BaseModifier
{
    [SerializeField]
    private float _damage;

    private HealthSystem _health;
    private Rigidbody2D _rigidbody;

    public override void BeginEffect(ModifiableActor target)
    {
        _target = target;
        _currentEffectTimes = 0;
        _health = _target.GetComponent<HealthSystem>();
        _rigidbody = _target.GetComponent<Rigidbody2D>();
    }

    public override IEnumerator GiveEffect(ModifiableActor target)
    {
        BeginEffect(target);
        while (_currentEffectTimes < EffectTimes)
        {   
            _health.TakeDamage(_damage);
            _rigidbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            _currentEffectTimes++;
            yield return new WaitForSeconds(1f);
        }
        EndEffect();
    }

    public override void EndEffect()
    {
        
    }
}
