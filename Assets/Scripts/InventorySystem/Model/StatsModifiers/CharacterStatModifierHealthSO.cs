using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/CharacterStatModifier")]
public class CharacterStatModifierHealthSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        throw new System.NotImplementedException();
        // Health health = character.GetComponent<Health>();
        // if (health != null)
        //     health.AddHealth((int)val);
    }
}
