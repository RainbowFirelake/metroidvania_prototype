using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory System/Inventory Objects", 
        fileName = "InventoryObjects")]
    public class InventoryObject : ScriptableObject
    {
        public ItemInfo defaultItemInfo;
        public List<ItemInfo> itemInfo = new List<ItemInfo>();

        public ItemInfo GetInfoByType(ItemType type)
        {
            foreach(var item in itemInfo)
            {
                if (item.itemType == type)
                {
                    return item;
                }
            }

            return defaultItemInfo;
        }
    }
}

