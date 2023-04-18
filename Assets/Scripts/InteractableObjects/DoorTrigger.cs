using UnityEngine;
using System;
using InventorySystem;

namespace InteractableObjects.Doors
{
    public class DoorTrigger : MonoBehaviour
    {
        public event Action OnDoorActivatorEnter;

        [SerializeField] private ItemType _itemToActivate;
        [SerializeField] private int _amountToActivate = 3;

        void OnTriggerEnter2D(Collider2D other)
        {
            var inventory = other.GetComponent<Inventory>();
            if (inventory && inventory.GetItemAmount(_itemToActivate) >= _amountToActivate)
            {
                OnDoorActivatorEnter?.Invoke();
            }
        }
    }
}

