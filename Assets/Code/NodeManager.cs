using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] GameObject UpNodePrefab;
    [SerializeField] GameObject DownNodePrefab;
    [SerializeField] GameObject LeftNodePrefab;
    [SerializeField] GameObject RightNodePrefab;

    [SerializeField] private int num = 10;
    
    [SerializeField] private float Speed = 5.0f;

    private GameObject[] UpNodes;
    private GameObject[] DownNodes;
    private GameObject[] LeftNodes;
    private GameObject[] RightNodes;
    
    private int upIndex = 0;
    private int downIndex = 0;
    private int leftIndex = 0;
    private int rightIndex = 0;
    
    public NodeInfo[] NodeInfos;

    public event Action NodeEnd;
    
    

    void Awake()
    {
        Init();
    }

    void Init()
    {
        UpNodes = new GameObject[num];
        DownNodes = new GameObject[num];
        LeftNodes = new GameObject[num];
        RightNodes = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            UpNodes[i] = Instantiate(UpNodePrefab, transform);
            UpNodes[i].GetComponent<Node>().Init(Speed);
            
            DownNodes[i] = Instantiate(DownNodePrefab, transform);
            DownNodes[i].GetComponent<Node>().Init(Speed);
            
            LeftNodes[i] = Instantiate(LeftNodePrefab, transform);
            LeftNodes[i].GetComponent<Node>().Init(Speed);
            
            RightNodes[i] = Instantiate(RightNodePrefab, transform);
            RightNodes[i].GetComponent<Node>().Init(Speed);
        }
    }

    public void PlayNode()
    {
        StartCoroutine(PlayN());
    }

    IEnumerator PlayN()
    {
        int i = 0;

        float t = Time.time;
        
        while (i < NodeInfos.Length)
        {
            if (NodeInfos[i].time <= Time.time - t)
            {
                switch (NodeInfos[i].direction)
                {
                    case NodeDirection.Up:
                        UpNodes[upIndex].GetComponent<Node>().MoveStart();
                        upIndex =  (upIndex + 1) % num;
                        break;
                    case NodeDirection.Down:
                        DownNodes[downIndex].GetComponent<Node>().MoveStart();
                        downIndex = (downIndex + 1) % num;
                        break;
                    
                    case NodeDirection.Left:
                        LeftNodes[leftIndex].GetComponent<Node>().MoveStart();
                        leftIndex = (leftIndex + 1) % num;
                        break;
                    
                    case NodeDirection.Right:
                        RightNodes[rightIndex].GetComponent<Node>().MoveStart();
                        rightIndex = (rightIndex + 1) % num;
                        break;
                }
                i++;
            }
            yield return null;
        }
        
        yield return new WaitForSeconds(2.0f);
        
        NodeEnd?.Invoke();
    }
}
