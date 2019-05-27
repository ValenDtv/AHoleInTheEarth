using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CursorControlOptions
{
    public GameObject Collector;
}

public class CursorControl : MonoBehaviour
{
    public CursorControlOptions cco;
    bool x = true;
    private GameObject iw;
    public static float timing = 1f;
    private bool isPaused = false;
    public static bool disableInventory = false;
    /**/ //    private Transform canvas;


    //private GameObject panel = GameObject.Find("Panel").GetComponent<GameObject>().;
    //private Light canvas;
    void Start()
    {
        iw = cco.Collector.GetComponent<GameObjectCollector>().GameObjects.InventWindow;
        /**/  //canvas = GameObject.Find("Canvas").transform;
              //canvas = GetComponent<Canvas>();
    }
    void Update()
    {
        Time.timeScale = timing;
        if (Input.GetKeyDown(KeyCode.Tab) && !disableInventory)
        {
            x = !x;
            isPaused = !isPaused;
            //Debug.Log(x);

            if (x)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                /**/  //           canvas.GetComponentInParent<Canvas>().enabled = false;
                iw.SetActive(false);
                timing = 1f;
                //panel.
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                /**/  //           canvas.GetComponentInParent<Canvas>().enabled = true;
                iw.SetActive(true);
                timing = 0f;
            }
        }
    }

}
