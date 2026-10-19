using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action OnTargEnable;
    public Stage1End end;
    public Scrolling scroll;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayGetTarget();
            end.StageEnd(true);
            scroll.Init(null, 0);
        }
    }

    void OnEnable()
    {
        scroll.OnOutOfCamera += FailStage;
        scroll.Init(null, GameManager.instance.CurSpeed * -1);
        OnTargEnable?.Invoke();
    }

    void OnDisable()
    {
        scroll.OnOutOfCamera -= FailStage;
    }

    public void FailStage(GameObject go)
    {
        GameManager.instance.PlayFail();
        end.StageEnd(false);
        scroll.Init(null, 0);
    }
}
