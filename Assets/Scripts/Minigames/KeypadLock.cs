using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadLock : MonoBehaviour
{
    private const string CORRECT_PASSWORD = "8314";
    private string password = "";
    public Text text_password;
    public GameObject panelForActivate;
    public GameObject[] objectsForDisable;


    public void PressKey(string key) 
    {
        password += key;
        text_password.text = password;
        if (password.Length == 4)
        {
            if (password == CORRECT_PASSWORD)
            {
                SuccessOpenDoor();
            }
            else
                password = "";
        }
        text_password.text = password;
    }

    private void SuccessOpenDoor()
    {

    }

    public void ActivateKeypadUI()
    {
        panelForActivate.SetActive(true);
        foreach (GameObject ob in objectsForDisable)
            ob.SetActive(false);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableKeypadUI()
    {
        panelForActivate.SetActive(false);
        foreach (GameObject ob in objectsForDisable)
            ob.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        text_password.text = "";
        
    }
    
    void Update()
    {

    }

    
}
