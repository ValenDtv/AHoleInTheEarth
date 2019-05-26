using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextWindow : MonoBehaviour
{
    GameObject canvastxt;
    private GameObjectCollector Collector;

    Canvas x;
    Image p;
    Text t;

    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        canvastxt = Collector.GameObjects.Canvastxt;
        Settext("adawdawdawd");
    }
    void Settext(string str)
    {
        x = canvastxt.GetComponentInChildren<Canvas>();
        p = x.GetComponentInChildren<Image>();
        t = p.GetComponentInChildren<Text>();
        t.text = str;
        //canvastxt.GetComponent<Canvas>().enabled = true;
    }

    public void ButtonOKClick()
    {
        canvastxt.GetComponent<Canvas>().enabled = false;
        Cursor.visible = false;
    }
}
