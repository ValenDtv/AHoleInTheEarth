using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTit : MonoBehaviour
{
    GameObject tit;
    private GameObjectCollector Collector;
    Canvas x;
    Image p;
    Text t;
    // Start is called before the first frame update
    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        tit = Collector.GameObjects.Tit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
