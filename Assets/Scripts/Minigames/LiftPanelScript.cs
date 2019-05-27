using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.CharacterController;

public class LiftPanelScript : MonoBehaviour
{
    CommentDialogue comment;
    SinglePhrase sp;
    GameObjectCollector Collector;
    public bool isOpen = false;
    public GameObject CameraCog;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        comment = this.gameObject.GetComponent<CommentDialogue>();
        sp = new SinglePhrase(Collector.GameObjects.Subtitles.GetComponent<Text>());
        player = Collector.GameObjects.Player;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        if (!isOpen)
            if (Inventary.ItemInHand == "Key")
            {
                isOpen = true;
                //Destroy(GameObject.Find("Key2"));
                Inventary.DeleteItem("Key");
                PlayerPrefs.SetString("Key", "IsNotHave");
                PlayerPrefs.SetString("LiftPanelOpen", "yes");
                PlayerPrefs.Save();
                StartCoroutine(sp.Say("Есть! Получилось открыть!", 4));
            }
            else
            {
                StartCoroutine(sp.Say("Закрыто! Здесь нужен ключ", 4));
            }
        else
        {
            if (Inventary.ItemInHand == "Gears")
            {
                player.GetComponent<vThirdPersonInput>().disable = true;
                CameraCog.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = true;
                CursorControl.disableInventory = true;
            }
            else
                comment.SendMessage("Start_dialog");
        }
    }
}
