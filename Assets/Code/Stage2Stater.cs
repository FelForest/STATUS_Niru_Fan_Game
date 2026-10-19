using System;
using UnityEngine;

public class Stage2Stater : MonoBehaviour
{
    // 시작 함수
    public void GameStart()
    {
        dialogueSystem.gameObject.SetActive(false);
        PlayClip();
        nodeManager.PlayNode();
    }
    
    // 사용한 사운드 클립
    public AudioClip clip;

    public void PlayClip()
    {
        GameManager.instance.PlayBGM(clip);
    }
    
    // 노드 올라오게 만들기
    public NodeManager nodeManager;
    
    
    
    // 판정
    // 콜리전으로 지속적으로 잃어나고 있는지 확인하면 되는거고
    // 판정은 각각이 맡아서 하는게 맞는듯
    
    [SerializeField] private DialogueSystem dialogueSystem;

    void OnEnable()
    {
        dialogueSystem.OnDialogueEnd += GameStart;
    }

    void OnDisable()
    {
        dialogueSystem.OnDialogueEnd -= GameStart;
    }

    private void Start()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.DialogueStart();
    }


    // 성공률 확인해서 다시하기, 다음 스테이지 넘기는거 나오게 하기
    // 다이얼로그 발생 시키기
    

}
