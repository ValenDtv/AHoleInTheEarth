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
    private bool isSelected = false; 
    private GameObject ActionPanel;
    private bool isMouseOver = false;
    string[] itemsinv = { "Revolver", "Gears", "Gear1", "Gear2", "Kapsula", "Key", "Key1", "Key2"};
    string[,] combinationsofoitems = { { "Gear1", "Gear2", "Gears" }, { "Key1", "Key2", "Key" } };


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
        if (!ActionPanel.activeSelf)
            isSelected = false;
        if (RightMouseButtonPressed && isMouseOver)
            ShowActionPanel(); 
    }

    private void InspectButtonCliked()
    {
        if (isSelected)
        {
            //StartCoroutine(ViewItem3d());
            Collector.GameObjects.ViewItem3D.SendMessage("View", this);
            //this.SendMessage("ViewItem3d");
            Collector.GameObjects.ActionPanel.SetActive(false);
        }
    }

    private void InHandButtonCliked()
    {
        if (isSelected)
        {
            Inventary.ItemInHand = this.gameObject.name;
            GameObject itemImage = GameObject.Instantiate(Resources.Load<GameObject>(this.gameObject.name));
            Collector.GameObjects.ItemInHand.GetComponent<Image>().sprite = itemImage.GetComponent<Image>().sprite;
            GameObject.Destroy(itemImage);
            Collector.GameObjects.ActionPanel.SetActive(false);
        }
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
            isSelected = true;
        }
        else
        {
            ActionPanel.SetActive(false);
        }
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

            if (distance > temp)
            {
                distance = temp;
                newCell = Inventary.content[i];
            }
            //if (distance > temp)
            //    if (!(
            //       Inventary.content[i].transform.Find(itemsinv[0]) || Inventary.content[i].transform.Find(itemsinv[1])
            //    || Inventary.content[i].transform.Find(itemsinv[2]) || Inventary.content[i].transform.Find(itemsinv[3])
            //    || Inventary.content[i].transform.Find(itemsinv[4]) || Inventary.content[i].transform.Find(itemsinv[5])
            //    || Inventary.content[i].transform.Find(itemsinv[6]) || Inventary.content[i].transform.Find(itemsinv[7])))
            //        {
            //            distance = temp;
            //            newCell = Inventary.content[i];
            //        }
            //        else
            //        {
            //            CombineItems(Inventary.content[i], newCell);
            //        }
        }
        if ((
                   newCell.transform.Find(itemsinv[0]) || newCell.transform.Find(itemsinv[1])
                || newCell.transform.Find(itemsinv[2]) || newCell.transform.Find(itemsinv[3])
                || newCell.transform.Find(itemsinv[4]) || newCell.transform.Find(itemsinv[5])
                || newCell.transform.Find(itemsinv[6]) || newCell.transform.Find(itemsinv[7])))
                {
                    if (!CombineItems(newCell, cell))
                        newCell = cell;
                }

        for (int i = 0; i < 2; i++)
        {
            temp = Vector2.Distance(transform.position, Inventary.content2[i].transform.position);
            if (distance > temp && !(Inventary.content2[i].transform.Find(itemsinv[0]) || Inventary.content2[i].transform.Find(itemsinv[1])
                || Inventary.content2[i].transform.Find(itemsinv[2]) || Inventary.content2[i].transform.Find(itemsinv[3])
                || Inventary.content2[i].transform.Find(itemsinv[4]) || Inventary.content2[i].transform.Find(itemsinv[5])
                || Inventary.content2[i].transform.Find(itemsinv[6]) || Inventary.content2[i].transform.Find(itemsinv[7])))
            {
                distance = temp;
                newCell = Inventary.content2[i];
            }
        }
        if (Vector2.Distance(transform.position, Inventary.content3[0].transform.position)
            < Vector2.Distance(transform.position, Inventary.content4[0].transform.position))
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


    bool CombineItems(Cell cellA, Cell cellB)
    {

        //bool z = Eq("Pumpkin", "Revolver");
        bool isCombined = false;
        //Cell cellA = Inventary.content2[0];
        //Cell cellB = Inventary.content2[1];
        switch (Eq(cellA, cellB))
        {
            case "Gears":
                {
                    GameObject it = GameObject.Find("GearsObject").GetComponent<ItemObject>().item;
                    Inventary.AddItem(it);
                    Destroy(GameObject.Find("GearsObject"));
                    //Destroy(GameObject.Find("Gear1"));
                    //Destroy(GameObject.Find("Gear2"));
                    Inventary.DeleteItem("Gear1");
                    Inventary.DeleteItem("Gear2");
                    PlayerPrefs.SetString("Gear1Object", "IsNotHave");
                    PlayerPrefs.SetString("Gear2Object", "IsNotHave");
                    isCombined = true;
                    break;
                }     
            case "Key":
                {
                    Inventary.DeleteItem("Key1");
                    Inventary.DeleteItem("Key2");
                    GameObject it = GameObject.Find("KeyObject").GetComponent<ItemObject>().item;
                    Inventary.AddItem(it);
                    Destroy(GameObject.Find("KeyObject"));
                    //Destroy(GameObject.Find("Key1"));
                    //Destroy(GameObject.Find("Key2"));

                    PlayerPrefs.SetString("Key1Object", "IsNotHave");
                    PlayerPrefs.SetString("Key2Object", "IsNotHave");
                    isCombined = true;
                    break;
                }
        }
        return isCombined;
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

    string Eq(Cell cellA, Cell cellB)
    {
        for (int i = 0; i < combinationsofoitems.GetLength(0); i++)
        {
            if (
                cellA.transform.Find(combinationsofoitems[i,0]) & canvas.transform.Find(combinationsofoitems[i, 1]) ||
                canvas.transform.Find(combinationsofoitems[i, 0]) & cellA.transform.Find(combinationsofoitems[i, 1])
                )
            {
                return combinationsofoitems[i, 2];
            }
        }
        return "";
    }
}
