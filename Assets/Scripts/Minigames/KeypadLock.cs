using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadLock : MonoBehaviour
{
    private const string CORRECT_PASSWORD = "8314";
    private string password = "";
    public Text text_password;
    private GameObjectCollector Collector;
    public GameObject panelForActivate;
    public GameObject[] objectsForDisable;
    public bool isOpen = false;
    SinglePhrase sp;


    public void PressKey(string key) 
    {
        if (password.Length >= 4)
            password = "";
        password += key;
        text_password.text = password;
        if (password.Length == 4)
        {
            if (password == CORRECT_PASSWORD)
            {
                password = "OPEN";
                SuccessOpenDoor();
            }
            else
                password = "LOCK";
        }
        text_password.text = password;
    }

    private void SuccessOpenDoor()
    {
        isOpen = true;
        PlayerPrefs.SetString("KeyPadIsOpen", "yes");
        PlayerPrefs.Save();
        StartCoroutine(sp.Say("Дверь открылась, отлично.", 4));
    }

    public void ActivateKeypadUI()
    {
        panelForActivate.SetActive(true);
        foreach (GameObject ob in objectsForDisable)
            ob.SetActive(false);
        Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CursorControl.disableInventory = true;
    }

    public void DisableKeypadUI()
    {
        panelForActivate.SetActive(false);
        foreach (GameObject ob in objectsForDisable)
            ob.SetActive(true);
        Collector.GameObjects.ThirdPersonCamera.GetComponent<Activate>().DisableInteraction = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CursorControl.disableInventory = false;
    }

    private void Awake()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        sp = new SinglePhrase(Collector.GameObjects.Subtitles.GetComponent<Text>());
    }

    void Start()
    {
        text_password.text = "";
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            DisableKeypadUI();
        }
    }

    
}
