using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    string loadingScene;
    void Start()
    {
        loadingScene = GameManager.instance.NextScene;
        SceneManager.LoadScene(loadingScene);
    }
}
