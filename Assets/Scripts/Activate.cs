using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Вешать на камеру
public class Activate : MonoBehaviour
{
    public GameObject item;

    private class Thing
    {
        private string name;
        private string description;


        public Thing(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }
    }

    float dis = 3f; //Максимальное расстояние от камеры до объекта
    List<Thing> inventory = new List<Thing>(); //Инвентарь
    CursorLockMode wantedMode;
    int radius = 1;
    int max_distance = 2;
    public int MaskNumber;
    int layerMask;

    void Start()
    {
        layerMask = 1 << MaskNumber;
    }

    void Update()
    {
        RaycastHit hit;
        Camera camera = GetComponent("vThirdPersonCamera").GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray, radius, out hit, max_distance, layerMask))
        {
            if (hit.collider.GetComponent<Outline>() != null)
            {
                hit.collider.GetComponent<Outline>().enabled = true;
                hit.collider.GetComponent<Outline>().lastTime = System.DateTime.Now;
                hit.collider.GetComponent<Outline>().lightOff = false;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.distance <= dis)
                {
                    switch (hit.collider.tag)
                    {
                        case "Interactive": //Интерактивный объект
                            hit.collider.gameObject.SendMessage("Start_dialog");
                            break;
                        case "Thing": //Вещь, которую можно поднять
                            //Thing thing = new Thing(hit.collider.name, "");
                            //inventory.Add(thing);
                            //Destroy(hit.collider.gameObject);
                            hit.collider.gameObject.SendMessage("AddI");
                            
                            break;
                        case "Character": //Персонаж
                                          //Активировать диалог
                            hit.collider.gameObject.SendMessage("Start_dialog");
                            break;
                    }
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    //RaycastHit hit;
        //    //Camera camera = GetComponent("vThirdPersonCamera").GetComponent<Camera>();
        //    //Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        //Debug.Log(hit.collider.name);
        //        if (hit.distance <= dis)
        //        {

        //            switch (hit.collider.tag)
        //            {
        //                case "Interactive": //Интерактивный объект
        //                    hit.collider.gameObject.SendMessage("Start_dialog");
        //                    break;
        //                case "Thing": //Вещь, которую можно поднять
        //                    //Thing thing = new Thing(hit.collider.name, "");
        //                    //inventory.Add(thing);
        //                    //Destroy(hit.collider.gameObject);
        //                    hit.collider.gameObject.SendMessage("AddI");

        //                    break;
        //                case "Character": //Персонаж
        //                                  //Активировать диалог
        //                    hit.collider.gameObject.SendMessage("Start_dialog");
        //                    break;
        //            }
        //        }
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Debug.Log("Инвентарь:");
        //    foreach (Thing item in inventory)
        //        Debug.Log(item.Name);
        //}
    }

}
