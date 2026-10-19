using UnityEngine;
using UnityEngine.UI;

public class StageProgress : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Awake()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    void Update()
    {
        slider.value = GameManager.instance.CurrentRate;
    }
    
}
