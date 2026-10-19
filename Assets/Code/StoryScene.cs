using UnityEngine;

public class StoryScene : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] private string nextScene;
    
    
    void OnEnable()
    {
        dialogueSystem.OnDialogueEnd += NextScene;
    }

    void OnDisable()
    {
        dialogueSystem.OnDialogueEnd -= NextScene;
    }

    public void NextScene()
    {
        GameManager.instance.SceneLoad(nextScene);
    }

    void Start()
    {
        dialogueSystem.DialogueStart();
    }
}
