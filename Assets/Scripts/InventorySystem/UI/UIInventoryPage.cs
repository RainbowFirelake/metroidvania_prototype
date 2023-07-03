using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InventorySystem.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private UIInventoryDescription _itemDescription;
        [SerializeField] private MouseFollower _mouseFollower;
        [SerializeField] private ItemActionPanel _actionPanel;

        private List<UIInventoryItem> _listOfUIItems = new List<UIInventoryItem>();

        private int _currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested,
            OnStartDragging;

        public event Action<int, int> OnSwapItems;

        void Awake()
        {
            Hide();
            _mouseFollower.Toggle(false);
            _itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                _listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void Hide()
        {
            _actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        private void HandleItemSelection(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleBeginDrag(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1) return;
            _currentlyDraggedItemIndex = index;
            HandleItemSelection(item);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            _mouseFollower.Toggle(true);
            _mouseFollower.SetData(sprite, quantity);
        }

        private void HandleSwap(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
            HandleItemSelection(item);
        }

        private void ResetDraggedItem()
        {
            _mouseFollower.Toggle(false);
            _currentlyDraggedItemIndex = -1;
        }

        private void HandleEndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }

        private void HandleShowItemActions(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }

            OnItemActionRequested?.Invoke(index);
        }

        public void UpdateData(int itemIndex, 
            Sprite itemImage, int itemQuantity)
        {
            if (_listOfUIItems.Count > itemIndex)
            {
                _listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        public void ResetSelection()
        {
            _itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void ShowItemActions(int itemIndex)
        {
            _actionPanel.Toggle(true);
            _actionPanel.transform.position = _listOfUIItems[itemIndex].transform.position;

        }

        public void AddAction(string actionName, Action performAction)
        {
            _actionPanel.AddButton(actionName, performAction);
        }

        private void DeselectAllItems()
        {
            foreach(var item in _listOfUIItems)
            {
                item.Deselect();
            }

            _actionPanel.Toggle(false);
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            _itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _listOfUIItems[itemIndex].Select();
        }

        public void ResetAllItems()
        {
            foreach (var item in _listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}
