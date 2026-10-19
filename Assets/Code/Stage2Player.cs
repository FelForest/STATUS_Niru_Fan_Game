using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Stage2Player : MonoBehaviour
{
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Down = Animator.StringToHash("Down");
    private static readonly int Up = Animator.StringToHash("Up");
    
    private Animator animator;

    [SerializeField] NodeJudgment  UpJudgment;
    [SerializeField] NodeJudgment LeftJudgment;
    [SerializeField] NodeJudgment RightJudgment;
    [SerializeField] NodeJudgment DownJudgment;

    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnWASD(InputValue value)
    {
        var dir = value.Get<Vector2>();
        SetValue(dir);
    }

    void SetValue(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            animator.SetBool(Left, true);
            animator.SetBool(Right, false);
            animator.SetBool(Down, false);
            animator.SetBool(Up, false);

            if (LeftJudgment.Judgment())
            {
                
            }
        }

        if (direction == Vector2.right)
        {
            animator.SetBool(Left, false);
            animator.SetBool(Right, true);
            animator.SetBool(Down, false);
            animator.SetBool(Up, false);

            if (RightJudgment.Judgment())
            {
                
            }
            
        }

        if (direction == Vector2.up)
        {
            animator.SetBool(Left, false);
            animator.SetBool(Right, false);
            animator.SetBool(Down, false);
            animator.SetBool(Up, true);

            if (UpJudgment.Judgment())
            {
                
            }
        }

        if (direction == Vector2.down)
        {
            animator.SetBool(Left, false);
            animator.SetBool(Right, false);
            animator.SetBool(Down, true);
            animator.SetBool(Up, false);

            if (DownJudgment.Judgment())
            {
                
            }
        }
    }
    
}
