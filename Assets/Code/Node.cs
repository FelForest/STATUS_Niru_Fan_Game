using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    // 원래 위치로 되돌아가는 함수
    // 움직일지 말지 정하는 함수
    // 업데이트로 돌린건데, 이거 60개 정도면 크게 문제 없을듯

    private float NodeSpeed;

    [SerializeField] private bool CanMove = false;
    
    private Vector3 StartPosition;

    private void Awake()
    {
        StartPosition =  transform.position;
    }

    public void Init(float speed)
    {
        NodeSpeed = speed;
    }

    public void MoveStart()
    {
        CanMove = true;
    }

    public void MoveStop()
    {
        CanMove = false;
        transform.position = StartPosition;
    }

    void Update()
    {
        if (CanMove)
        {
            transform.Translate(0 , NodeSpeed * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Limit"))
        {
            MoveStop();
        }
    }
}
