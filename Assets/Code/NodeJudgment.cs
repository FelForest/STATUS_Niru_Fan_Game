using UnityEngine;

public class NodeJudgment : MonoBehaviour
{
    private Collider2D collider2D;

    private bool check;

    private Node node;

    void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Node"))
        {
            node = other.GetComponent<Node>();
            check =  true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Node"))
        {
            node = null;
            check = false;
        }
    }

    public bool Judgment()
    {
        if (check)
        {
            node.MoveStop();
            return true;
        }
        return false;
    }
    
}
