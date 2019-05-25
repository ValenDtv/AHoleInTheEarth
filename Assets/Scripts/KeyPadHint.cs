using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;

public class KeyPadHint : MonoBehaviour
{
    float time = 0f;
    bool active = false; 
    public GameObject Hint;
    GameObjectCollector Collector;

    // Start is called before the first frame update
    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && time > 1)
        {
            Hint.SetActive(false);
            Collector.GameObjects.Player.SetActive(true);
            active = false;
            time = 0;
        }
    }

    public void Action()
    {
        Hint.SetActive(true);
        Collector.GameObjects.Player.SetActive(false);
        active = true;
    }
}
