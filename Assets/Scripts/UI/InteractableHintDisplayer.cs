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
    private Camera _mainCamera;
    [SerializeField]
    private Interactor _interactor;

    private IWorldInteractable _interactableInfo;

    void OnEnable()
    {
        _interactor.OnInteractablePositionDetected += UpdateInteractablePanel;
    }

    void Update()
    {
        if (_interactableInfo != null)
        {
            text.text = _interactableInfo.InteractionName;
            var ydelta = _interactableInfo.t.localScale.y;
            Vector3 v = new Vector3(_interactableInfo.t.position.x, 
                _interactableInfo.t.position.y + ydelta);
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(v);
            _interactableHintPanel.position = screenPos;
        }
    }

    private void UpdateInteractablePanel(IWorldInteractable interactable)
    {
        _interactableInfo = interactable;
    }
}
