using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEmptyObject : MonoBehaviour
{
    public GameObject item;
    


    void Update()
    {
        CheckPickUp();
    }

    void CheckPickUp()
    {
        GameObject x = GameObject.Find("Generate");
        x.GetComponent<Button>().onClick.AddListener(delegate { Add(); });
        
    }

    void Add()
    {
        print("да");
        /*
        if (Inventary.AddItem(item))
        {
            Destroy(gameObject);
        }
        */
    }
    
}
