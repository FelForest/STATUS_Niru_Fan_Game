using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;


public class Stage1End : MonoBehaviour
{
    // 스테이지1 끝나는거 관리용 스크립트

    [SerializeField] private float delayTime = 2.0f;
    public GameObject[] UI;
    public CinemachineCamera camera;
    public string nextScene = "Title";
    
    public DialogueSystem dialogue;
    public DialogueSystem faildialogue;

    public GameObject nextbutton;
    public GameObject againbutton;

    public GameObject Target;

    private bool t = false;

    void OnEnable()
    {
        dialogue.OnDialogueEnd += showNextButton;
        faildialogue.OnDialogueEnd += showAgainButton;
    }

    void OnDisable()
    {
        dialogue.OnDialogueEnd -= showNextButton;
        faildialogue.OnDialogueEnd -= showAgainButton;
    }
    
    
    // UI 제거
    void HideUI()
    {
        foreach (GameObject go in UI)
        {
            go.SetActive(false);
        }
    }
    
    // 카메라 줌인
    void ZoominCamera()
    {
        camera.gameObject.SetActive(true);
    }
    
    // 신 넘어가도록 만들기
    public void LoadScene()
    {
        GameManager.instance.SceneLoad(nextScene);
    }
    // 

    public void ReLoadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);
        SceneManager.LoadScene(currentScene);
        GameManager.instance.GameEnd();
    }
    

    public void StageEnd(bool success)
    {
        HideUI();
        ZoominCamera();
        GameManager.instance.GameEnd();
        StartCoroutine(Delay(success));
    }

    IEnumerator Delay(bool success)
    {
        yield return new WaitForSeconds(delayTime);
        
        // 대사 치는거 나와야함
        // 이건 다른 스크립트로 하자
        ShowDialogue(success);
    }

    void ShowDialogue(bool success)
    {
        if (success)
        {
            dialogue.gameObject.SetActive(true);
            dialogue.DialogueStart();
        }
        else
        {
            faildialogue.gameObject.SetActive(true);
            faildialogue.DialogueStart();
        }
    }

    void showNextButton()
    {
        nextbutton.SetActive(true);
    }

    void showAgainButton()
    {
        againbutton.SetActive(true);
    }

    void Update()
    {
        if (GameManager.instance.CurrentRate >= 0.85f && !t)
        {
            AppearedTarget();
            t = true;
        }
    }

    void AppearedTarget()
    {
        Target.SetActive(true);
    }
}
