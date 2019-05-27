using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.transform.Translate(Vector3.up * 30 * Time.deltaTime);
        if (Text.transform.position.y > Screen.height + Screen.height/2)
            SceneManager.LoadScene("MainMenu");
    }
}
