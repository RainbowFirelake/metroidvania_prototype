using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractableHintDisplayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private RectTransform _interactableHintPanel;
    [SerializeField]
    private Interactor _interactor;

    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();        
    }

    void OnEnable()
    {
        _interactor.OnInteractablePositionDetected += UpdateInteractablePanel;
        _interactor.OnInteractableMissing += Disable;
    }

    private void UpdateInteractablePanel(Transform t, string actionName)
    {
        Enable();
        var vector = new Vector3(t.position.x, t.position.y + t.localScale.y);
        _transform.position = vector;
        text.text = actionName;
    }

    private void Enable()
    {
        _interactableHintPanel.gameObject.SetActive(true);
    }

    private void Disable()
    {
        _interactableHintPanel.gameObject.SetActive(false);
    }
}
