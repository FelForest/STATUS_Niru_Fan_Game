using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public Button button;
    
    public string nextScene;
    
    void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        GameManager.instance.SceneLoad(nextScene);
    }
}
