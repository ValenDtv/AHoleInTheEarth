using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Invector.CharacterController;
using UnityEngine.UI;


public class Item : MonoBehaviour, IDragHandler, IEndDragHandler
{
    GameObject iw;
    [HideInInspector]
    public Cell cell;
    private Transform canvas;
    string[] itemsinv = { "Revolver", "Gears", "Gear1", "Gear2", "Kapsula", "Key", "Key1", "Key2"};


    void Start()
    {
        iw = Inventary.iw;
        cell = GetComponentInParent<Cell>();
        canvas = GameObject.Find("Canvas").transform;
        Debug.Log(canvas);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //
        transform.SetParent(canvas);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
       
        float distance = float.MaxValue;
        Cell newCell = cell;
        float temp;

        bool x, y;
        
        
        //yield return "";
        for (int i = 0; i < Inventary.content.Length; i++)
        {
            temp = Vector2.Distance(transform.position, Inventary.content[i].transform.position);


            if (distance > temp && !(Inventary.content[i].transform.Find(itemsinv[0]) || Inventary.content[i].transform.Find(itemsinv[1]) || Inventary.content[i].transform.Find(itemsinv[2]) || Inventary.content[i].transform.Find(itemsinv[3]) || Inventary.content[i].transform.Find(itemsinv[4]) || Inventary.content[i].transform.Find(itemsinv[5]) || Inventary.content[i].transform.Find(itemsinv[6]) || Inventary.content[i].transform.Find(itemsinv[7])))
            {
                distance = temp;
                newCell = Inventary.content[i];
            }
        }
        for (int i = 0; i < 2; i++)
        {
            temp = Vector2.Distance(transform.position, Inventary.content2[i].transform.position);
            if (distance > temp && !(Inventary.content2[i].transform.Find(itemsinv[0]) || Inventary.content2[i].transform.Find(itemsinv[1]) || Inventary.content2[i].transform.Find(itemsinv[2]) || Inventary.content2[i].transform.Find(itemsinv[3]) || Inventary.content2[i].transform.Find(itemsinv[4]) || Inventary.content2[i].transform.Find(itemsinv[5]) || Inventary.content2[i].transform.Find(itemsinv[6]) || Inventary.content2[i].transform.Find(itemsinv[7])))
            {
                distance = temp;
                newCell = Inventary.content2[i];
            }
        }
        if (Vector2.Distance(transform.position, Inventary.content3[0].transform.position) < Vector2.Distance(transform.position, Inventary.content4[0].transform.position))
        {
            temp = Vector2.Distance(transform.position, Inventary.content3[0].transform.position);
            y = true;
        }
        else
        {
            temp = Vector2.Distance(transform.position, Inventary.content4[0].transform.position);
            y = false;
        }
        
        
        x = distance <= temp ? false : true;
       
        if (x & y && !(Inventary.content3[0].transform.Find(itemsinv[0]) || Inventary.content3[0].transform.Find(itemsinv[1]) || Inventary.content3[0].transform.Find(itemsinv[2]) || Inventary.content3[0].transform.Find(itemsinv[3]) || Inventary.content3[0].transform.Find(itemsinv[4]) || Inventary.content3[0].transform.Find(itemsinv[5]) || Inventary.content3[0].transform.Find(itemsinv[6]) || Inventary.content3[0].transform.Find(itemsinv[7])))
        {
            print("работает3");
            distance = temp;
            newCell = Inventary.content3[0];
            Destroy(gameObject);
            string nameobj = gameObject.name;
            string nameObj = nameobj + "Object";
            GameObject xxx = Instantiate(Resources.Load<GameObject>(nameObj));
            xxx.name = nameObj;
            Vector3 gamerpos = GameObject.Find("vThirdPersonController").transform.position;
            Quaternion gamerrot = GameObject.Find("vThirdPersonController").transform.rotation;
            xxx.transform.SetPositionAndRotation(gamerpos + Vector3.one, gamerrot);
            xxx.GetComponent<ItemObject>().item = Resources.Load<GameObject>(nameobj);

            //GameObject cam = GameObject.Find("CameraInv");
        }
        //реализация 3d просмотра

        if (x & !y && !(Inventary.content4[0].transform.Find(itemsinv[0]) || Inventary.content4[0].transform.Find(itemsinv[1]) || Inventary.content4[0].transform.Find(itemsinv[2]) || Inventary.content4[0].transform.Find(itemsinv[3]) || Inventary.content4[0].transform.Find(itemsinv[4]) || Inventary.content4[0].transform.Find(itemsinv[5]) || Inventary.content4[0].transform.Find(itemsinv[6]) || Inventary.content4[0].transform.Find(itemsinv[7])))
        {
            print("работает4");
            vThirdPersonInput inputscr = GameObject.Find("vThirdPersonController").GetComponent<vThirdPersonInput>();
            
            inputscr.disable = true;
            distance = temp;
            newCell = Inventary.content4[0];

            GameObject cam = GameObject.Find("CameraInv");
            cam.GetComponent<Camera>().enabled = true;
            Vector3 campos = GameObject.Find("CameraInv").transform.position;

            GameObject camk = GameObject.Find("CameraKaps");
            cam.GetComponent<Camera>().enabled = true;
            Vector3 camkpos = GameObject.Find("CameraKaps").transform.position;
            
            string name = gameObject.name + "3D";
            GameObject obj3d = Instantiate(Resources.Load<GameObject>(name));
            Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up);
            obj3d.transform.SetPositionAndRotation(campos + Vector3.forward/2, rotationY);

            

            GameObject tm = GameObject.Find("vThirdPersonCamera");
            //tm.GetComponent<CursorControl>().timing = 1.0f;
            Time.timeScale = 1.0f;
            print(obj3d);
            tm.GetComponent<CursorControl>().enabled = false;
            //**       //canvas.GetComponentInParent<Canvas>().enabled = false;
            iw.SetActive(false);
            this.gameObject.GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(2.0f);
            //print(name);
            if (name == "Kapsula3D")
            {
                print("Это оно");

                
                GameObject objkaps = Instantiate(Resources.Load<GameObject>(name));
                objkaps.transform.SetPositionAndRotation(camkpos + Vector3.forward / 4, rotationY);

                camk.GetComponent<Camera>().enabled = true;
            }
            //camk.GetComponent<Camera>().enabled = false;
            Destroy(obj3d);

            inputscr.disable = false;
            //float t = new Time.deltaTime;
            tm.GetComponent<CursorControl>().enabled = true;
            //canvas.GetComponentInParent<Canvas>().enabled = true;
            iw.SetActive(true);
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.SetActive(true);
            cam.GetComponent<Camera>().enabled = false;
            Time.timeScale = 0.0f;
            //print(tm);
            //cam.GetComponent<Camera>().enabled = false;
            /**/
        }
        
        cell = newCell;
        transform.SetParent(cell.transform);
        transform.position = cell.transform.position;
        transform.localScale = Vector2.one;
    }
    

}
