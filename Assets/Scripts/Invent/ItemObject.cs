using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public GameObject item;

  

    void Update()
    {
        CheckPickUp();
    }

    void CheckPickUp()
    {
        if(Vector3.Distance(transform.position, GameObject.Find("vThirdPersonController").transform.position) < 1f)
        {
            
            if (Inventary.AddItem(item))
            {
                Destroy(gameObject);
            }
            
            
        }
    }
}
