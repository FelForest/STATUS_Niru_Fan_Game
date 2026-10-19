using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Stage1Starter : MonoBehaviour
{
    // 스테이지 1의 스타터를 위한거

    [SerializeField] private float delayTime;
    public TMPro.TextMeshProUGUI text;
    public CinemachineCamera camera;

    public GameObject[] UI;
    
    public DialogueSystem dialogueSystem;
    
    public void GameStart()
    {
        dialogueSystem.gameObject.SetActive(false);
        StartCoroutine(ShowUIAndDelay());
    }
    
    // 스테이지 시작하면, 카메라 조절 및, UI 호출
    
    // 카메라 다 조절 되면 UI 호출하도록 하기 -> 이거 그냥 시간으로 해야겠다
    
    IEnumerator ShowUIAndDelay()
    {
        camera.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayTime);
        
        ShowUI();

        StartCoroutine(CountDown());


    }

    void ShowUI()
    {
        foreach (GameObject go in UI)
        {
            go.SetActive(true);
        }
    }

    IEnumerator CountDown()
    {
        int count = 3;
        
        // 사운드 재생
        GameManager.instance.PlayCountDown();
        
        while (count > 0)
        {
            text.text = count.ToString();
            yield return new WaitForSeconds(1);
            
            count--;
        }
        
        // 카운트 다운 UI 사라지게 하기
        text.gameObject.SetActive(false);
        
        
        // 게임 시작
        GameManager.instance.GameStart();
    }

    private void Start()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1.0f);
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.DialogueStart();
    }

    void OnEnable()
    {
        dialogueSystem.OnDialogueEnd += GameStart;
    }

    void OnDisable()
    {
        dialogueSystem.OnDialogueEnd -= GameStart;
    }
}
