using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Вешать на камеру
public class Activate : MonoBehaviour
{
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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Camera camera = GetComponent("vThirdPersonCamera").GetComponent<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= dis)
                {
                    switch (hit.collider.tag)
                    {
                        case "Interactive": //Интерактивный объект
                            hit.collider.gameObject.SendMessage("Start_dialog");
                            break;
                        case "Thing": //Вещь, которую можно поднять
                            Thing thing = new Thing(hit.collider.name, "");
                            inventory.Add(thing);
                            Destroy(hit.collider.gameObject);
                            break;
                        case "Character": //Персонаж
                                          //Активировать диалог
                            hit.collider.gameObject.SendMessage("Start_dialog");
                            break;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Инвентарь:");
            foreach (Thing item in inventory)
                Debug.Log(item.Name);
        }
    }

}
