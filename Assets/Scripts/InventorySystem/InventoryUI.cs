using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private InventoryObject _inventoryObjects;

    private void Start()
    {
        
    }

    void OnEnable()
    {
        _playerInventory.OnInventoryUpdate += UpdateUI;
    }

    private void UpdateUI(Dictionary<ItemType, int> items)
    {
        foreach (var item in items)
        {
            var itemInfo = _inventoryObjects.GetInfoByType(item.Key);
            Debug.Log("inventory updated");
        }
    }
}
