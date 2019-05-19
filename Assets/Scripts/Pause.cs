using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public KeyCode pauseInput = KeyCode.P;

    private GameObjectCollector Collector;
    private bool isPaused = false;
    GameObject pausecanvas;
    // Start is called before the first frame update
    void Start()
    {

        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        pausecanvas = Collector.GameObjects.PauseCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        Paused();
    }

    //Пауза
    public void Paused()
    {
        if (Input.GetKeyDown(pauseInput))
        {
            if (isPaused)
            {
                
                CursorControl.timing = 1f;
                pausecanvas.GetComponent<Canvas>().enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
            }
            else
            {
                Debug.Log("pau");
                isPaused = true;
                CursorControl.timing = 0f;
                pausecanvas.GetComponent<Canvas>().enabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } 
        }
    }
    public void ButtonEscClick()
    {
        CursorControl.timing = 1f;
        pausecanvas.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }
}
