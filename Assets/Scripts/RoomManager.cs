using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] cutsceneObjects;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("wasCutscene"))
        {
            foreach (GameObject o in cutsceneObjects)
            {
                o.SetActive(false);
            }
        } else
        {
            PlayerPrefs.SetString("wasCutscene", "RealWas");
        }
    }
    
}
