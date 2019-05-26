using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inventary : MonoBehaviour
{
    public static GameObject iw;
    public static string ItemInHand = "";
    public GameObject GOCollector;
    private GameObjectCollector Collector;

    [SerializeField]
    private int capacity;
    [SerializeField]
    public Transform view;
    public static Cell[] content;

    public static Cell[] content2;

    public static Cell[] content3;

    public static Cell[] content4;

    [SerializeField]
    public Transform view2;

    [SerializeField]
    public Transform view3;

    [SerializeField]
    public Transform view4;

    void Awake()
    {
        content = new Cell[capacity];
        content2 = new Cell[2];
        content3 = new Cell[1];
        content4 = new Cell[1];
        CreateCell();
        CreateCell2();
        CreateCell3();
        CreateCell4();
        iw = this.gameObject;
        Collector = GOCollector.GetComponent<GameObjectCollector>();
        this.gameObject.SetActive(false);
    }

    void CreateCell()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject cell = Instantiate(Resources.Load<GameObject>("Cell"));

            cell.transform.SetParent(view);
            cell.transform.localScale = Vector2.one;
            cell.name = string.Format("Cell [{0}]", i);

            content[i] = cell.GetComponent<Cell>();
        }
    }
    void CreateCell2()
    { 
        for (int i = 0; i < 2; i++)
        {
            GameObject cell2 = Instantiate(Resources.Load<GameObject>("Cell"));
            cell2.transform.SetParent(view2);
            cell2.transform.localScale = Vector2.one;
            cell2.name = string.Format("CellChange [{0}]", i);

            content2[i] = cell2.GetComponent<Cell>();

        }

        
    }

    void CreateCell3()
    {
        
            GameObject cell3 = Instantiate(Resources.Load<GameObject>("Urna"));
            cell3.transform.SetParent(view3);
            cell3.transform.localScale = Vector2.one;
            cell3.name = string.Format("CellOut");


            content3[0] = cell3.GetComponent<Cell>();
        
    }


    void CreateCell4()
    {
        GameObject cell3d = Instantiate(Resources.Load<GameObject>("_3D"));
        cell3d.transform.SetParent(view4);
        cell3d.transform.localScale = Vector2.one;
        cell3d.name = string.Format("Cell3d");
        content4[0] = cell3d.GetComponent<Cell>();
    }

    public static bool AddItem(GameObject item)
    {
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i].transform.childCount == 0)
            {
                GameObject newItem = Instantiate(item);

                newItem.transform.SetParent(content[i].transform);
                newItem.transform.localScale = Vector2.one;
                newItem.transform.position = newItem.transform.parent.position;
                newItem.name = item.name;
                newItem.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                newItem.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                newItem.GetComponent<Item>().cell = content[i];
                content[i].item = newItem;
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (ItemInHand != "")
            Collector.GameObjects.PlayerHand.SendMessage("Show");
    }

    private void OnDisable()
    {
        Collector.GameObjects.ActionPanel.SetActive(false);
        Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = false;
    }

    private void OnEnable()
    {
        Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = true;
    }

    public static void DeleteItem(string name)
    {
        foreach (Cell cell in content)
        {
            if (cell.item != null)
                if (cell.item.name == name)
                {
                    if (Inventary.ItemInHand == name)
                        Inventary.ItemInHand = "";
                    Destroy(cell.item);
                    break;
                }
        }
    }

    //private void OnEnable()
    //{
    //    if (ItemInHand != "")
    //        Collector.GameObjects.PlayerHand.SendMessage("Show");
    //}
}
