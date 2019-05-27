using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ShutDownVideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ShutDuwn();
    }

    private void ShutDuwn()
    {
        this.gameObject.GetComponent<VideoPlayer>().Pause();
        GameObjectCollector.Collector.GetComponent<GameObjectCollector>().GameObjects.Load.SetActive(true);
        SceneManager.LoadScene("L2_1");
    }

}
