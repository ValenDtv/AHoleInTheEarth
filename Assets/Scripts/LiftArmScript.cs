using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftArmScript : MonoBehaviour
{
    public bool liftFixed = false;
    CommentDialogue comment;

    // Start is called before the first frame update
    void Start()
    {
        comment = this.gameObject.GetComponent<CommentDialogue>();
    }

    public void Action()
    {
        if (liftFixed)
        {
            SceneManager.LoadScene("UnityRoom");
        }
        else
        {
            comment.SendMessage("Start_dialog");
        }
    }
}
