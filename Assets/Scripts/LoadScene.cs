using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName = "";

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
    }
}
