using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Dictionary<ItemType, int> _items; 


        private void Start()
        {
            _items = new Dictionary<ItemType, int>();
        }

        public void AddItem(ItemType type, int amount)
        {
            if (_items.ContainsKey(type))
            {
                _items[type] += amount;
            }
            else 
            {
                _items.Add(type, amount);
            }
        }
        
        public int GetItemAmount(ItemType type)
        {
            if (!_items.ContainsKey(type)) return 0;
            return _items[type];
        }
    }
}
