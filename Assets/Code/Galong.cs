using System.Collections;
using UnityEngine;

public class Galong : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private static readonly int Walk = Animator.StringToHash("Walk");

    private float speed;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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


    void OnSpeedChange(float value)
    {
        speed = value;
        if (speed > 0.0f)
        {
            anim.SetBool(Walk, true);
        }
        else
        {
            anim.SetBool(Walk, false);
        }
    }

    public void Jump(float delay, float jumpPower)
    {
        StartCoroutine(JumpDelay(delay, jumpPower));
    }

    IEnumerator JumpDelay(float delay, float power)
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocityY = 0.0f;
        rb.AddForceY(power, ForceMode2D.Impulse);
        anim.SetBool(Walk, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (speed > 0.0f)
            {
                anim.SetBool(Walk, true);
            }
        }
    }
    
    // 개롱이들 멈추게 하기
    public void GalogStop()
    {
        // 애니메이션 멈추고
        anim.SetBool(Walk, false);
        // 현재 플레이어 속도 가져와서 일정시간 뒤로 밀기
        StartCoroutine(StopGalog());
        
        // 화면 밖으로 나가면 끝
    }

    IEnumerator StopGalog()
    {
        float time = 0;

        float Speed = GameManager.instance.CurSpeed * -1;
        StartCoroutine(Invincibility());
        while (time < 5.0f)
        {
            time += Time.deltaTime;
            gameObject.transform.Translate(Speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
    
    IEnumerator Invincibility()
    {
        float time = 0.0f;
        float timestep = 0.2f;
        int i = 0;

        while (time < 5.0f)
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
    }

    void ChangeAlpha(float alpha)
    {
        var color = sr.color;
        color.a = alpha;
        sr.color = color;
    }
    
    
}