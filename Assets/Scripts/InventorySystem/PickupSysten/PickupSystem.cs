using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Model;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO _inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item == null) return;

        int reminder = _inventoryData.AddItem(item.InventoryItem, item.Quantity);
        if (reminder == 0)
            item.DestroyItem();
        else
            item.Quantity = reminder;
    }
}
