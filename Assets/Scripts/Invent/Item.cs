using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Invector.CharacterController;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject iw;
    private GameObjectCollector Collector;
    [HideInInspector]
    public Cell cell;
    private Transform canvas;
    private bool RightMouseButtonPressed;
    private GameObject ActionPanel;
    private bool isMouseOver = false;
    string[] itemsinv = { "Revolver", "Gears", "Gear1", "Gear2", "Kapsula", "Key", "Key1", "Key2"};


    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        iw = Inventary.iw;
        ActionPanel = Collector.GameObjects.ActionPanel;
        cell = GetComponentInParent<Cell>();
        canvas = Collector.GameObjects.Canvas.transform;
        //GameObject.Find("dialogue_canvas").transform;
        Collector.GameObjects.InspectButton.GetComponent<Button>().onClick.AddListener(InspectButtonCliked);
        Collector.GameObjects.InHandButton.GetComponent<Button>().onClick.AddListener(InHandButtonCliked);
        Debug.Log(canvas);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    void Update()
    {
        RightMouseButtonPressed = Input.GetKeyDown(KeyCode.Mouse1);
        if (RightMouseButtonPressed && isMouseOver)
            ShowActionPanel();
    }

    private void InspectButtonCliked()
    {
        //StartCoroutine(ViewItem3d());
        Collector.GameObjects.ViewItem3D.SendMessage("View", this);
        //this.SendMessage("ViewItem3d");
        Collector.GameObjects.ActionPanel.SetActive(false);
    }

    private void InHandButtonCliked()
    {
        Inventary.ItemInHand = this.gameObject.name;
        GameObject itemImage = GameObject.Instantiate(Resources.Load<GameObject>(this.gameObject.name));
        Collector.GameObjects.ItemInHand.GetComponent<Image>().sprite = itemImage.GetComponent<Image>().sprite;
        GameObject.Destroy(itemImage);
        Collector.GameObjects.ActionPanel.SetActive(false);
    }

    private void ShowActionPanel()
    {
        if (!ActionPanel.activeSelf)
        {
            ActionPanel.GetComponent<RectTransform>().position = Input.mousePosition;
            //ActionPanel.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
            ActionPanel.GetComponent<RectTransform>().position = new Vector3(ActionPanel.GetComponent<RectTransform>().position.x +
                ActionPanel.GetComponent<RectTransform>().rect.width / 2 - 25,
                ActionPanel.GetComponent<RectTransform>().position.y - ActionPanel.transform.GetComponent<RectTransform>().rect.height / 2);
            //ActionPanel.GetComponent<RectTransform>().localPosition = new Vector3(ActionPanel.GetComponent<RectTransform>().rect.position.x +
            //    ActionPanel.GetComponent<RectTransform>().rect.width / 2,
            //   ActionPanel.GetComponent<RectTransform>().rect.position.y - ActionPanel.transform.GetComponent<RectTransform>().rect.height / 2);
            ActionPanel.SetActive(true);
        }
        else
            ActionPanel.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //
        if (!Input.GetKey(KeyCode.Mouse0))
            return;
        transform.SetParent(canvas);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Input.GetKeyUp(KeyCode.Mouse0))
            return;
        //StartCoroutine(TestCoroutine());
        MoveItem();
    }


    void MoveItem()
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
       
        //if (x & y && !(Inventary.content3[0].transform.Find(itemsinv[0]) || Inventary.content3[0].transform.Find(itemsinv[1]) || Inventary.content3[0].transform.Find(itemsinv[2]) || Inventary.content3[0].transform.Find(itemsinv[3]) || Inventary.content3[0].transform.Find(itemsinv[4]) || Inventary.content3[0].transform.Find(itemsinv[5]) || Inventary.content3[0].transform.Find(itemsinv[6]) || Inventary.content3[0].transform.Find(itemsinv[7])))
        //{
        //    print("работает3");
        //    distance = temp;
        //    newCell = Inventary.content3[0];
        //    Destroy(gameObject);
        //    string nameobj = gameObject.name;
        //    string nameObj = nameobj + "Object";
        //    GameObject xxx = Instantiate(Resources.Load<GameObject>(nameObj));
        //    xxx.name = nameObj;
        //    Vector3 gamerpos = GameObject.Find("vThirdPersonController").transform.position;
        //    Quaternion gamerrot = GameObject.Find("vThirdPersonController").transform.rotation;
        //    xxx.transform.SetPositionAndRotation(gamerpos + Vector3.one, gamerrot);
        //    xxx.GetComponent<ItemObject>().item = Resources.Load<GameObject>(nameobj);

        //    //GameObject cam = GameObject.Find("CameraInv");
        //}
        //реализация 3d просмотра

        
        
        cell = newCell;
        transform.SetParent(cell.transform);
        transform.position = cell.transform.position;
        transform.localScale = Vector2.one;
    }
    

}
