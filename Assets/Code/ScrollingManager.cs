using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class ScrollingManager : MonoBehaviour
{
    // 스크롤링 스크립트를 관리하기 위한 매니저
    // 배경을 랜덤하게 주기위해서 만든것
    // 
    
    // 필요한거
    // - 배경 프리팹, 풀, 배치 크기, 속도(이건 겜매한테 받아올거임), 스프라이트
    
    [SerializeField] private GameObject[] scrollingPrefabs;
    
    
    [SerializeField] private Sprite[] citySprites;
    
    [Header("속도")]
    [SerializeField] private float speedRate;
    
    [Header("소환 갯수")] [SerializeField] private int spawnCount;
    [Header("오브젝트 크기")] [SerializeField] private float ObjectSize;

    private int prenum = -1;
    private int curnum = 0;

    private bool a = false;

    public Target target;
    
    private GameObject[] scrollingObjects;

    void OnEnable()
    {
        GameManager.instance.OnStageStart += ScrollingStart;
        GameManager.instance.OnStageEnd += ScrollingEnd;

        if (target)
        {
            target.OnTargEnable += StopScolling;
        }
    }

    void OnDisable()
    {
        GameManager.instance.OnStageStart -= ScrollingStart;
        GameManager.instance.OnStageEnd -= ScrollingEnd;
        if (target)
        {
            target.OnTargEnable -= StopScolling;
        }
    }
    
    

    void SpawnScrolling()
    {
        GameObject scrollingPrefab = scrollingPrefabs[Random.Range(0, scrollingPrefabs.Length)];
        if (scrollingPrefab)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                scrollingPrefab = scrollingPrefabs[Random.Range(0, scrollingPrefabs.Length)];
                GameObject go = Instantiate(scrollingPrefab, transform);
                go.transform.Translate(new Vector3(i * ObjectSize, 0.0f, 0.0f));
                Init(go, true);
                
                scrollingObjects[i] = go;
            }
            a = true;
        }
    }
    
    void Awake()
    {
        scrollingObjects = new GameObject[spawnCount];
        SpawnScrolling();
    }

    void SetPosition(GameObject go)
    {
        go.transform.Translate(ObjectSize * spawnCount,  0.0f, 0.0f);
        Init(go, true);
    }

    void Init(GameObject go, bool check)
    {
        Scrolling s = go.GetComponent<Scrolling>();

        if (check)
        {
            if (citySprites.Length > 0)
            {
                curnum = Random.Range(0, citySprites.Length);

                while (curnum == prenum)
                {
                    curnum = Random.Range(0, citySprites.Length);
                }
                
                s.Init(citySprites[curnum], GameManager.instance.CurSpeed * speedRate * -1);
                prenum = curnum;
            }
            else
            {
                s.Init(null, GameManager.instance.CurSpeed * speedRate * -1);
            }
        }
        else
        {
            s.Init(null, GameManager.instance.CurSpeed * speedRate * -1);
        }

        
        
        

        if (!a)
        {
            s.OnOutOfCamera +=  SetPosition;
        }
    }


    void ScrollingStart()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Init(scrollingObjects[i], false);
        }
        
    }

    void ScrollingEnd()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Init(scrollingObjects[i], false);
        }
    }

    void StopScolling()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            if (Mathf.Abs(scrollingObjects[i].transform.position.x - target.transform.position.x) < 3.0f)
            {
                scrollingObjects[i].SetActive(false);
            }
        }
    }
}
            