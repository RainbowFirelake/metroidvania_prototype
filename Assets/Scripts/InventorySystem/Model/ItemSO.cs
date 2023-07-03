using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; private set; }
        
        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; private set; } = 1;
        [field: SerializeField] public string Name { get; private set; }

        [field: TextArea] 
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite ItemImage { get; private set; }
    }
}