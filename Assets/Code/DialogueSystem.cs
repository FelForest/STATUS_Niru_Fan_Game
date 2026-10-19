using System;
using System.Collections;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    // 다디얼 로그를 띄우기 위한 것
    
    // 순서에 맞게 대사 띄우고 마지막에 가면, 이벤트 발행
    public event Action OnDialogueEnd;
    
    public string[] Dialogues;

    public float showTime = 1.0f;
    
    public TMPro.TextMeshProUGUI text;
    
    public void DialogueStart()
    {
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        int count = 0;
        while (count < Dialogues.Length)
        {
            text.text = Dialogues[count];
            count++;
            yield return new WaitForSeconds(showTime);
        }
        
        OnDialogueEnd?.Invoke();
        
    }
    
}
