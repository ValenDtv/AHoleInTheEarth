using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disappearance : MonoBehaviour
{
    public GameObject GOCollector;
    private GameObjectCollector Collector;
    private Image PlayerHand;
    private Image ItemInHand;

    // Start is called before the first frame update
    void Start()
    {
        Collector = GOCollector.GetComponent<GameObjectCollector>();
        PlayerHand = Collector.GameObjects.PlayerHand.GetComponent<Image>();
        ItemInHand = Collector.GameObjects.ItemInHand.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHand.color.a > 0f)
            PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g, PlayerHand.color.a - 0.0065104166666667f);
        if (ItemInHand.color.a > 0f)
            ItemInHand.color = new Color(ItemInHand.color.r, ItemInHand.color.b, ItemInHand.color.g, ItemInHand.color.a - 0.0166666666666667f);
    }

    public void Show()
    {
        PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g, 0.390625f);
        ItemInHand.color = new Color(ItemInHand.color.r, ItemInHand.color.b, ItemInHand.color.g, 1);
    }
}
