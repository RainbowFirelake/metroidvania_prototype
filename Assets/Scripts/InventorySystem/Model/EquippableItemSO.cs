using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Model
{
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character)
        {
            throw new System.NotImplementedException();
        }
    }
}
