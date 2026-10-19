using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float JumpPower = 5.0f;
    
    private Animator anim;
    private float playerSpeed;
    private int jumpCount = 2;
    
    private static readonly int Walk = Animator.StringToHash("Walk");
    
    public Galong[] Galongs;
    public int HP = 3;
    [SerializeField] private float GalongDelay = 0.2f;
    
    private SpriteRenderer sr;
    private bool CanDamamge = true;
    // 무적 시간 1초
    private float invincibilityDuration = 1.0f;
    public Target target;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GameManager.instance.OnSpeedChange += OnSpeedChange;
    }

    void OnDisable()
    {
        GameManager.instance.OnSpeedChange -= OnSpeedChange;
    }

    void Start()
    {
        //GameManager.instance.GameStart();
    }
    
    public void OnJump(InputValue value)
    {
        Debug.Log("OnJump");

        if (!GameManager.instance.Running)
        {
            return;
        }
        
        if (jumpCount <= 0)
        {
            return;
        }
        
        rb.linearVelocityY = 0.0f;
        rb.AddForceY(JumpPower, ForceMode2D.Impulse);
        
        jumpCount--;
        
        anim.SetBool(Walk, false);
        
        GameManager.instance.PlayJumpSound();

        GalongJump();
    }

    void GalongJump()
    {
        for (int i = 0; i < HP; i++)
        {
            Galongs[i].Jump((i + 1) *  GalongDelay, JumpPower);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (playerSpeed > 0.0f)
            {
                anim.SetBool(Walk, true);
            }
            jumpCount = 2;
        }
    }

    void OnSpeedChange(float value)
    {
        playerSpeed = value;
        if (playerSpeed > 0.0f)
        {
            anim.SetBool(Walk, true);
        }
        else
        {
            anim.SetBool(Walk, false);
        }
    }

    public void Crash()
    {
        // 체력 줄고, 일정 시간 동안 무적, 깜빡거임
        if (HP > 0)
        {
            if (CanDamamge)
            {
                HP--; // 2, 1, 0 이기 때문에 가능 HP로 indxe 대싱 사용하자
                Debug.Log(HP);
            
                // 개롱이들 멈추게 해야함
                Galongs[HP].GalogStop();
            
                StartCoroutine(Invincibility());
                CameraShake();
            }
        }
        else
        {
            // 목숨이 다 끝났다는 이야기니, 스테이지 실패 실행
            target.FailStage(gameObject);
            anim.SetBool(Walk, false);
        }
    }

    IEnumerator Invincibility()
    {
        CanDamamge = false;

        float time = 0.0f;
        float timestep = 0.2f;
        int i = 0;

        while (time < invincibilityDuration)
        {
            time += timestep;
            i++;
            if (i % 2 == 0)
            {
                ChangeAlpha(1.0f);
            }
            else
            {
                ChangeAlpha(0.5f);
            }
            yield return new WaitForSeconds(timestep);
        }
        
        ChangeAlpha(1.0f);
        
        CanDamamge = true;
    }

    void ChangeAlpha(float alpha)
    {
        var color = sr.color;
        color.a = alpha;
        sr.color = color;
    }

    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;

    [SerializeField] private float noiseTime = 0.1f;
    [SerializeField] private float noiseAmplitude = 5.0f;
    [SerializeField] private float noiseFrequency = 1.0f;

    void CameraShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        noise.AmplitudeGain =  noiseAmplitude;
        noise.FrequencyGain =  noiseFrequency;
        yield return new WaitForSeconds(noiseTime);
        noise.AmplitudeGain = 0.0f;
        noise.FrequencyGain = 0.0f;
    }

}
