using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public GameObject item;
    void Start()
    {

    }
  

    void Update()
    {
        //CheckPickUp();
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
    void AddI()
    {
        if (Inventary.AddItem(item))
        {
            //Destroy(gameObject);
            //Сделать вызов диалога нормально

            GameObjectCollector.Collector.GetComponent<GameObjectCollector>().GameObjects.Player.SendMessage("Start_thing_dialog", gameObject.name);
          
             //GameObject.Find("person_controller");
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
