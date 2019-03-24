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
    bool x = true;
   // [SerializeField]
    /**/ //    private Transform canvas;
    private GameObject iw;
    public float timing;
    public bool isPaused = false;

    public CursorControlOptions cco;
    //private GameObject panel = GameObject.Find("Panel").GetComponent<GameObject>().;
    //private Light canvas;
    void Start()
    {
        print("dadad");
        print(cco);
        iw = cco.Collector.GetComponent<GameObjectCollector>().GameObjects.InventWindow;
  /**/  //canvas = GameObject.Find("Canvas").transform;
        timing = 1f;
        //canvas = GetComponent<Canvas>();
    }
    void Update()
    {
        Time.timeScale = timing;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            x = !x;
            isPaused = !isPaused;
            //Debug.Log(x);
        }
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
