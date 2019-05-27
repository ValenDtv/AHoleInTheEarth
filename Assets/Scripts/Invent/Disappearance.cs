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
    private float showtime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Collector = GOCollector.GetComponent<GameObjectCollector>();
        PlayerHand = Collector.GameObjects.PlayerHand.GetComponent<Image>();
        ItemInHand = Collector.GameObjects.ItemInHand.GetComponent<Image>();
        PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g, 0);
        ItemInHand.color = new Color(ItemInHand.color.r, ItemInHand.color.b, ItemInHand.color.g, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (showtime > 0f)
        {
            float previoutime = showtime;
            float delataTime = Time.deltaTime;
            showtime -= delataTime;
            PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g,
                PlayerHand.color.a - (PlayerHand.color.a/showtime)*delataTime);
            ItemInHand.color = new Color(ItemInHand.color.r, ItemInHand.color.b, ItemInHand.color.g,
                ItemInHand.color.a - (ItemInHand.color.a/showtime)*delataTime);
            //if (PlayerHand.color.a > 0f)
            //PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g, PlayerHand.color.a - 0.0065104166666667f);
            //if (ItemInHand.color.a > 0f)
            //ItemInHand.color = new Color(ItemInHand.color.r, ItemInHand.color.b, ItemInHand.color.g, ItemInHand.color.a - 0.0166666666666667f);
        }
    }

    public void Show()
    {
        PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g, 0.390625f);
        //PlayerHand.color = new Color(PlayerHand.color.r, PlayerHand.color.b, PlayerHand.color.g, 1f);
        ItemInHand.color = new Color(ItemInHand.color.r, ItemInHand.color.b, ItemInHand.color.g, 1f);
        showtime = 2f;
    }
}
