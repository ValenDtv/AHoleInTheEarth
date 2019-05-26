using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text.RegularExpressions;

public class SaveLoadScript : MonoBehaviour

{
    GameObjectCollector Collector;
    string[] items = { "Gear1Object", "Gear2Object", "GearsObject", "Key1Object", "Key2Object", "KeyObject", "object_revolver" };
    string[] UnityRoomItems = { "Revolver" };
    string[] L2_1Items = { "" };
    string[] L2_2Items = { "" };
    

    // Start is called before the first frame update
    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        //if (SceneManager.GetActiveScene().name != "MainMenu")
          //  PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        CheckSave(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckSave(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                if (PlayerPrefs.HasKey("CurrentScene"))
                {
                    Collector.GameObjects.ButtonContinue.SetActive(true);
                    Collector.GameObjects.ButtonContinue.GetComponent<LoadScene>().sceneName = PlayerPrefs.GetString("CurrentScene");
                }
                break;
            case "UnityRoom":
                checkItems(UnityRoomItems);
                if (PlayerPrefs.HasKey("CurrentScene"))
                    if (PlayerPrefs.GetString("CurrentScene") == sceneName)
                    checkPlayerPosition();
                    else
                        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
                break;
            case "L2_1":
                checkItems(L2_1Items);
                if (PlayerPrefs.HasKey("CurrentScene"))
                    if (PlayerPrefs.GetString("CurrentScene") == sceneName)
                        checkPlayerPosition();
                    else
                        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
                checkKeyPad();
                checkL2_1appearance();
                checkEva();
                break;
            case "L2_2":
                checkItems(L2_2Items);
                if (PlayerPrefs.HasKey("CurrentScene"))
                    if (PlayerPrefs.GetString("CurrentScene") == sceneName)
                        checkPlayerPosition();
                    else
                        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
                break;
        }
    }

    public Vector3 StringToVector3(string vector)
    {
        vector = vector.Trim(new char[] {'(', ')' });
        // string[] k = vector.Split(',', ' ');
        //float[] v = vector.Split(','' ').Select(n => (float)System.Convert.ToDouble(n)).ToArray<float>();
        float[] v = Regex.Split(vector, ", ").Select(n => (float)System.Convert.ToDouble(n)).ToArray<float>();
        return new Vector3(v[0], v[1], v[2]);
    }

    private void checkItems(string[] sceneItems)
    {
        foreach (string item in sceneItems)
            if (PlayerPrefs.HasKey(item))
                if (PlayerPrefs.GetString(item) == "IsNotHave")
                    Destroy(GameObject.Find(item));
        foreach (string item in items)
            if (PlayerPrefs.HasKey(item))
                if (PlayerPrefs.GetString(item) == "IsHave")
                    GameObject.Find(item).SendMessage("AddI");
    }

    private void checkPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
            Collector.GameObjects.Player.transform.localPosition = StringToVector3(PlayerPrefs.GetString("PlayerPosition"));
        //if (PlayerPrefs.HasKey("PlayerRotation"))
           // Collector.GameObjects.Player.transform.rotation =  new Quaternion(StringToVector3(PlayerPrefs.GetString("PlayerRotation"));
    }

    private void checkKeyPad()
    {
        if (PlayerPrefs.HasKey("KeyPadIsOpen"))
            if (PlayerPrefs.GetString("KeyPadIsOpen") == "Yes")
                Collector.GameObjects.KeyPad.GetComponent<KeypadLock>().isOpen = true;
    }

    private void checkEva()
    {
        int s = 1;
        if (PlayerPrefs.HasKey("Eva"))
             s = 2; // Поменять номер диалога Евы.
    }

    private void checkL2_1appearance()
    {
        int s = 1;
        if (PlayerPrefs.HasKey("L2_1FirstAppearance"))
            if (PlayerPrefs.GetString("L2_1FirstAppearance") == "Yes")
            {
                //Изменить координаты игрока
                //
            }
    }

}
