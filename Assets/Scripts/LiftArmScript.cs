using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftArmScript : MonoBehaviour
{
    public bool liftFixed = false;
    CommentDialogue comment;
    GameObjectCollector Collector;

    // Start is called before the first frame update
    void Start()
    {
        comment = this.gameObject.GetComponent<CommentDialogue>();
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
    }

    public void Action()
    {
        if (liftFixed)
        {
            Load("UnityRoom");
        }
        else
        {
            comment.SendMessage("Start_dialog");
        }
    }

    private void Load(string name)
    {
        Collector.GameObjects.Load.SetActive(true);
        SceneManager.LoadScene(name);
    }
}
