
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public GameObject Text;
    public GameObject CanvasTit;
    private bool start = false;
    SinglePhrase sp;

    public AudioSource mainSource;
    public AudioSource titles;
    // Start is called before the first frame update
    void Start()
    {
        sp = new SinglePhrase(GameObject.Find("subtitles").GetComponent<Text>());
    }

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            Text.transform.Translate(Vector3.up * 30 * Time.deltaTime);
                    if (Text.transform.position.y > Screen.height + Screen.height/2)
					{
						PlayerPrefs.DeleteAll();
						PlayerPrefs.Save();
                        SceneManager.LoadScene("MainMenu");
					}
        }
        
    }

    public void Action()
    {
        if (Inventary.ItemInHand != "Revolver")
        {
            StartCoroutine(sp.Say("Снаружи опасно. Нужно взять револьвер.", 4));
            return;
        } else
        if (Inventary.ItemInHand == "Revolver")
        {
            titles.Play();
            mainSource.mute = true;
            //GetComponent<AudioSource>().Play();
            CanvasTit.GetComponent<Canvas>().enabled = true;
            start = true;
        }
        
    }
}
