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

    // Start is called before the first frame update
    void Start()
    {
        comment = this.gameObject.GetComponent<CommentDialogue>();
    }

    public void Action()
    {
        if (SceneManager.GetActiveScene().name == "L2_1")
            if (keypad.isOpen)
                SceneManager.LoadScene(SceneName);
            else
            {
                comment.SendMessage("Start_dialog");
                keypad.ActivateKeypadUI();
            }
        else
            SceneManager.LoadScene(SceneName);
    }
}
