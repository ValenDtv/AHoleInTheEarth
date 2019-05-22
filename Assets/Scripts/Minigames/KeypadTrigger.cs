using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public KeypadLock KeypadLock;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            KeypadLock.ActivateKeypadUI();
        }
    }
}
