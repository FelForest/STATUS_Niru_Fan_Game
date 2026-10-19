using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // 나중에 Info로 받아오도록 하든지 해야지
    public StageInfo[] StageInfo;

    [SerializeField] private int currentStageNum;

    public bool Running;

    private float currentTime;
    public float CurrentRate {get; private set;}
    
    public event Action<float> OnSpeedChange;
    private float curSpeed;

    public event Action OnStageStart;
    public event Action OnStageEnd;

    private AudioManager audioManager;
    
    public float CurSpeed
    {
        get => curSpeed;
        set
        {
            if (curSpeed != value)
            {
                curSpeed = value;
                OnSpeedChange?.Invoke(value);
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Instance of GameManager");
            
            Application.targetFrameRate = 60;
            
            audioManager = GetComponent<AudioManager>();
            SetScreen();
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Destroy of GameManager");
        }
    }

    void Update()
    {
        if (Running)
        {
            currentTime += Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0, StageInfo[currentStageNum].StageTime);
            CurrentRate = currentTime / StageInfo[currentStageNum].StageTime;
        }
    }

    public void GameStart()
    {
        currentTime = 0;
        Running = true;
        
        CurSpeed = StageInfo[currentStageNum].PlayerSpeed;
        CurrentRate = 0;
        
        OnStageStart?.Invoke();
    }

    public void GameEnd()
    {
        Running = false;
        CurSpeed = 0;
        currentTime = 0;
        CurrentRate = 0;
        
        OnStageEnd?.Invoke();
    }

    public string NextScene {get; private set;}
    public void SceneLoad(string sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadScene(NextScene);
    }

    public void PlayJumpSound()
    {
        audioManager.PlayJumpSound();
    }

    public void PlayObstruction(AudioClip clip)
    {
        audioManager.PlayObstruction(clip);
    }

    public void PlayCountDown()
    {
        audioManager.PlayCountDown();
    }

    public void PlayGetTarget()
    {
        audioManager.PlayGetTarget();
    }

    public void PlayFail()
    {
        audioManager.PlayFail();
    }

    public void PlayBGM(AudioClip clip)
    {
        audioManager.PlayBGM(clip);
    }

    void SetScreen()
    {
        Screen.SetResolution(1280, 720, false);
    }
}
