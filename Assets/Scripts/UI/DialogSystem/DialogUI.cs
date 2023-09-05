using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _choiceButtonsTexts;
    [SerializeField]
    private GameObject _dialoguePanel;
    [SerializeField]
    private TMP_Text _dialogueText;

    private Story _currentStory;
    private bool _isDialoguePlaying = false;

    private void Start()
    {
        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        _isDialoguePlaying = true;
        _dialoguePanel.SetActive(true);

        if (_currentStory.canContinue)
        {
            _dialogueText.text = _currentStory.Continue();
        }        
        else 
        {
            ExitDialogueMode();
        }
    }

    public void ExitDialogueMode()
    {
        _isDialoguePlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueText.text = "";
    }
}
