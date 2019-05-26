using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName = "";
    GameObjectCollector Collector;

    private void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        Load("UnityRoom");
    }

    public void LoadGame()
    {
        if (sceneName != "")
            Load(sceneName);
    }

    private void Load(string name)
    {
        Collector.GameObjects.Load.SetActive(true);
        SceneManager.LoadScene(name);
    }
}
