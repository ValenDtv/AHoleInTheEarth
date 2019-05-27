using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public GameObject Text;
    public GameObject CanvasTit;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Text.transform.Translate(Vector3.up * 30 * Time.deltaTime);
                    if (Text.transform.position.y > Screen.height + Screen.height/2)
                        SceneManager.LoadScene("MainMenu");
        }
        
    }

    public void Action()
    {
        CanvasTit.GetComponent<Canvas>().enabled = true;
        start = true;
    }
}
