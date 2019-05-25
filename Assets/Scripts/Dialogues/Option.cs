using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Option : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool is_clicked = false;
    Sprite black;
    Sprite white;

    private void Start()
    {
        black = Resources.Load<Sprite>("BlackSprite");
        white = Resources.Load<Sprite>("WhiteSprite");
        this.gameObject.GetComponent<Image>().sprite = black;
        this.gameObject.GetComponentInChildren<Text>().color = Color.white;
    }

    // Update is called once per frame
    public void Set_click()
    {
        is_clicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //isMouseOver = true;
        this.gameObject.GetComponent<Image>().sprite = white;
        this.gameObject.GetComponentInChildren<Text>().color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().sprite = black;
        this.gameObject.GetComponentInChildren<Text>().color = Color.white;
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<Image>().sprite = black;
        this.gameObject.GetComponentInChildren<Text>().color = Color.white;
    }
}
