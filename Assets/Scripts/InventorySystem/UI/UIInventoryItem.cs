using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace InventorySystem.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text quantityTxt;
        [SerializeField] private Image _borderImage;

        public event Action<UIInventoryItem> OnItemClicked;
        public event Action<UIInventoryItem> OnItemDroppedOn;
        public event Action<UIInventoryItem> OnItemBeginDrag;
        public event Action<UIInventoryItem> OnItemEndDrag;
        public event Action<UIInventoryItem> OnRightMouseBtnClick;

        private bool _empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            this._itemImage.gameObject.SetActive(false);
            _empty = true;
        }

        public void Deselect()
        {
            _borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            _empty = false;
        }

        public void Select()
        {
            _borderImage.enabled = true;
        } 

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_empty) return;

            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}