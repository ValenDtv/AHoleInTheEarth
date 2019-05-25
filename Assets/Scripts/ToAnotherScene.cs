using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAnotherScene : MonoBehaviour
{
    public string SceneName;
    public KeypadLock keypad;
    private GameObjectCollector Collector;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Action()
    {
        if (keypad.isOpen)
            SceneManager.LoadScene(SceneName);
        else
            keypad.ActivateKeypadUI();
    }
}
