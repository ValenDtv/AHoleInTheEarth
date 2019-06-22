using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAnotherScene : MonoBehaviour
{
    public string SceneName;
    public KeypadLock keypad;
    private GameObjectCollector Collector;
    CommentDialogue comment;
    private string CurrentScene;

    // Start is called before the first frame update
    void Start()
    {
        comment = this.gameObject.GetComponent<CommentDialogue>();
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        CurrentScene = SceneManager.GetActiveScene().name;
    }

    public void Action()
    {
        if (CurrentScene == "L2_1")
            if (keypad.isOpen)
                Load(SceneName);
            else
            {
                comment.SendMessage("Start_dialog");
                keypad.ActivateKeypadUI();
            }
        else
           Load(SceneName);
    }

    private void Load(string name)
    {
        Collector.GameObjects.Load.SetActive(true);
        SceneManager.LoadScene(name);
    }
}
