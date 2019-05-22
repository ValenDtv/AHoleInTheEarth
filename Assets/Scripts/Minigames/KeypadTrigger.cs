using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public KeypadLock keypadLock;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" &&  !keypadLock.isOpen)
        {
            keypadLock.ActivateKeypadUI();
        }
    }
}
