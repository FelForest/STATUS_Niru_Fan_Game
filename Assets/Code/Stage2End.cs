using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stage2End : MonoBehaviour
{
    [SerializeField] private DialogueSystem dialogueSystem;
    
    [SerializeField] private NodeManager nodeManager;
    
    [SerializeField] private Button nextButton;

    private bool end = false;

    void OnEnable()
    {
        nodeManager.NodeEnd += StartEnd;
        dialogueSystem.OnDialogueEnd += ShowNextButton;
    }

    void OnDisable()
    {
        nodeManager.NodeEnd -= StartEnd;
        dialogueSystem.OnDialogueEnd -= ShowNextButton;
    }

    void StartEnd()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.DialogueStart();
    }

    void ShowNextButton()
    {
        dialogueSystem.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }
    
}
