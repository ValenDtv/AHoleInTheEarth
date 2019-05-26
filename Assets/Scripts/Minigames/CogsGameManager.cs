using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogsGameManager : MonoBehaviour
{
    public CogBehavior cog1;
    public CogBehavior cog2;

    void Start()
    {
        
    }

    void Update()
    {
        if (cog1.isRotate && cog2.isRotate)
        {
            cog1.StopRotation();
            cog2.StopRotation();
            WinAction();
        }
    }

    private void WinAction()
    {

    }
}
