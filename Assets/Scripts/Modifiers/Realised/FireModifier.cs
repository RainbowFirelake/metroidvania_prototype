using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new FireModifier", menuName = "Modifiers/New FireModifier")]
public class FireModifier : BaseModifier
{
    [SerializeField]
    private float _damage;

    public override void BeginEffect(ModifiableActor target)
    {
        
    }

    public override void GiveEffect(ModifiableActor target)
    {
        target.Health.TakeDamage(_damage);
    }
    

    public override void EndEffect()
    {
        
    }
}
