using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateItem : MonoBehaviour
{
    //public Transform contch;
    //public GameObject item;

    //public Sprite spr;
    void Start()
    {
        
        CreateButton();
        //CreateImg();
        
    }
    /*
    private void CreateImg()
    {
        GameObject spr = Instantiate(Resources.Load<GameObject>("hands"));
    }
    */
    void Update()
    {
        

    }

    void CreateButton()
    {
       //GameObject newButton = Instantiate(Resources.Load<GameObject>("_3D"));
        GameObject newButton = new GameObject("A", typeof (Image), typeof (Button), typeof (LayoutElement));
        //newButton.transform.SetParent(contch);

        //spr = Instantiate(Resources.Load<Sprite>("hands"));
        //newButton.GetComponent<Button>().image.sprite = spr;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { ButPress(); });

    }

    void ButPress()
    {
        
        print("pressed");
        bool x = Eq("Gear1", "Gear2");
        bool y = Eq("Key1", "Key2");
        //bool z = Eq("Pumpkin", "Revolver");
        
        Cell cellA = Inventary.content2[0];
        Cell cellB = Inventary.content2[1];
        
        if(x)
        {
            GameObject it = GameObject.Find("GearsObject").GetComponent<ItemObject>().item;
            Inventary.AddItem(it);
            Destroy(GameObject.Find("GearsObject"));
            Destroy(GameObject.Find("Gear1"));
            Destroy(GameObject.Find("Gear2"));
        }
        else if(y)
        {
            GameObject it = GameObject.Find("KeyObject").GetComponent<ItemObject>().item;
            Inventary.AddItem(it);
            Destroy(GameObject.Find("KeyObject"));
            Destroy(GameObject.Find("Key1"));
            Destroy(GameObject.Find("Key2"));
        }
        /*
        else if (z)
        {
            GameObject it = GameObject.Find("ScorpioObject").GetComponent<ItemObject>().item;
            Inventary.AddItem(it);
            Destroy(GameObject.Find("ScorpioObject"));
            Destroy(GameObject.Find("Pumpkin"));
            Destroy(GameObject.Find("Revolver"));
        }
        */
    }

    bool Eq(string a, string b)
    {
        Cell cellA = Inventary.content2[0];
        Cell cellB = Inventary.content2[1];
        if (
            cellA.transform.Find(a) & cellB.transform.Find(b) ||
            cellB.transform.Find(a) & cellA.transform.Find(b)
            )
        {
            return true;

        }
        else return false;
    } 
}
