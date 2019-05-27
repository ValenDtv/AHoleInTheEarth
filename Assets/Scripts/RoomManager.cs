using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] cutsceneObjects;
    public GameObject[] toTurnOn;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("WasCutscene"))
        {
            foreach (GameObject o in cutsceneObjects)
            {
                o.SetActive(false);
            }
            foreach (GameObject o in toTurnOn)
            {
                o.SetActive(true);
            }
        } else
        {
            PlayerPrefs.SetString("WasCutscene", "RealWas");
        }
    }
    
}
