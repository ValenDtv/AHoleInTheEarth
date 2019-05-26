using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    GameObjectCollector Collector;
    GameObject player;

    private void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        player = Collector.GameObjects.Player;
    }

    public void Quit()
    {
        PlayerPrefs.SetString("PlayerPosition", player.transform.localPosition.ToString());
        //PlayerPrefs.SetString("PlayerRotation", player.transform.position.ToString());
        Application.Quit();
        // убивает процесс Юнити
        //System.Diagnostics.Process.GetCurrentProcess().Kill();

    }
}
