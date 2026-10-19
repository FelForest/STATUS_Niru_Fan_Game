using System;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
    private Scrolling scroll;
    [SerializeField] AudioClip obstructionClip;

    void Awake()
    {
        scroll = GetComponent<Scrolling>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            

            if (p.HP > 0)
            {
                GameManager.instance.PlayObstruction(obstructionClip);
            }
            
            p.Crash();
        }
    }

    void OnEnable()
    {
        scroll.OnOutOfCamera += stopScrolling;
    }

    void OnDisable()
    {
        scroll.OnOutOfCamera -= stopScrolling;
    }

    void stopScrolling(GameObject go)
    {
        if (GameManager.instance.CurrentRate >= 0.95f)
        {
            go.SetActive(false);
        }
    }
    
    
    
}
