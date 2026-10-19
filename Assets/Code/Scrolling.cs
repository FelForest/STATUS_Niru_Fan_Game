using System;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    // 배경이 움직이도록 하는 스크립트
    // 화면 밖으로 나가면 반환 됨
    
    // 필요한거
    // - 화면 밖으로 나갔는지 판단하는 것, 속도, 나갔을때 발행할 이벤트, 배경용 스프라이트
    
    // 화면 밖에 나갈시 호출될 반환용 이벤트
    public Action<GameObject> OnOutOfCamera;
    public float outPosition;
    
    
    // 속도 ... 외부에서 접근해야함
    [field: SerializeField]
    public float ScrollSpeed { get; set; }
    
    // 컴포넌트
    SpriteRenderer spriteRenderer;
    
    public void Init(Sprite image, float speed)
    {
        if (image != null)
        {
            spriteRenderer.sprite = image;
        }
        ScrollSpeed = speed;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // 활성화 되면 무조건 움직이고 있을 거여서 상관 없을듯
    void Update()
    {
        if (GameManager.instance.Running)
        {
            gameObject.transform.Translate(ScrollSpeed * Time.deltaTime, 0, 0);

            if (gameObject.transform.position.x < outPosition)
            {
                OnOutOfCamera?.Invoke(gameObject);
            }
        }
    }
}
