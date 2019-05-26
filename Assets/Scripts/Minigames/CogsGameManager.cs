using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.CharacterController;

public class CogsGameManager : MonoBehaviour
{
    public CogBehavior cog1;
    public CogBehavior cog2;
    SinglePhrase sp;
    public GameObject CameraCog;
    GameObjectCollector Collector;
    private GameObject player;
    bool isWin = false;
    GameObject liftPanel;
    float time = 0f;

    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        sp = new SinglePhrase(Collector.GameObjects.Subtitles.GetComponent<Text>());
        player = player = Collector.GameObjects.Player;
        liftPanel = Collector.GameObjects.LiftPanel;
    }

    void Update()
    {
        if (isWin)
            return;
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && time < 0.5)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CameraCog.SetActive(false);
            player.GetComponent<vThirdPersonInput>().disable = false;
            Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = false;
            CursorControl.disableInventory = false;
            time = 0f;
        }
        if (cog1.isRotate && cog2.isRotate)
        {
            cog1.StopRotation();
            cog2.StopRotation();
            WinAction();
        }
    }

    private void WinAction()
    {
        isWin = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CameraCog.SetActive(false);
        player.GetComponent<vThirdPersonInput>().disable = false;
        Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = false;
        StartCoroutine(sp.Say("Бинго! Теперь лифит работает.", 4));
        liftPanel.layer = 0;
        Collector.GameObjects.LiftArm.GetComponent<LiftArmScript>().liftFixed = true;
        PlayerPrefs.SetString("LiftFixed", "yes");
        Destroy(GameObject.Find("Gears"));
        PlayerPrefs.SetString("Gears", "IsNotHave");
        CursorControl.disableInventory = false;
    }
}
