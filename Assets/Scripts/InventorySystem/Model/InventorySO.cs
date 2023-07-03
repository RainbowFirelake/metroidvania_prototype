using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace InventorySystem.Model
{       
    [CreateAssetMenu(fileName = "InventorySO", menuName = "Inventory System/new Inventory")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> _inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            _inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (!item.IsStackable)
            {
                for (int i = 0; i < _inventoryItems.Count; i++)
                {
                    while (quantity > 0 && !IsInventoryFull())
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChanges();
                    return quantity;
                }
            }
            
            quantity = AddStackableItem(item, quantity);
            InformAboutChanges();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };
            
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            
            return 0;
        }

        private bool IsInventoryFull()
        {
            foreach (var item in _inventoryItems)
            {
                if (item.IsEmpty) return false;
            }

            return true;
        }

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;

                if (_inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = 
                        _inventoryItems[i].item.MaxStackSize - _inventoryItems[i].quantity;
                    if (quantity > amountPossibleToTake)
                    {
                        _inventoryItems[i] = _inventoryItems[i]
                            .ChangeQuantity(_inventoryItems[i].item.MaxStackSize);
                        
                        quantity -= amountPossibleToTake;
                    }
                    else 
                    {
                        _inventoryItems[i] = _inventoryItems[i]
                            .ChangeQuantity(_inventoryItems[i].quantity + quantity);
                        InformAboutChanges();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;
                
                returnValue[i] = _inventoryItems[i];
            }

            return returnValue;
        }

        internal InventoryItem GetItemAt(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = _inventoryItems[itemIndex_1];
            _inventoryItems[itemIndex_1] = _inventoryItems[itemIndex_2];
            _inventoryItems[itemIndex_2] = item1;
            InformAboutChanges();
        }

        private void InformAboutChanges()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (_inventoryItems.Count > itemIndex)
            {
                if (_inventoryItems[itemIndex].IsEmpty)
                    return;
                
                int reminder = _inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    _inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else 
                    _inventoryItems[itemIndex] = _inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);
                
                InformAboutChanges();
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };
    }
}