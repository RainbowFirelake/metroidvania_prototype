using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.UI;
using UnityEngine.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _mainCam;

    [SerializeField] private UIInventoryItem _item;

    private void Awake()
    {
        _canvas = transform.root.GetComponent<Canvas>();
        _mainCam = Camera.main;
        _item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        _item.SetData(sprite, quantity);
    }

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            Input.mousePosition,
            _canvas.worldCamera,
            out position
        );

        transform.position = _canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
