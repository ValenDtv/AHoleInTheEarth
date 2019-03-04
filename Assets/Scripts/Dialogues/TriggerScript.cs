using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public bool Activated = false;

    private void OnTriggerEnter(Collider other)
    {
        Activated = true;
    }
}
