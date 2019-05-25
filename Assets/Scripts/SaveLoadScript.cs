using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SaveLoadScript : MonoBehaviour

{
    GameObjectCollector Collector;
    string[] items = { "Gear1", "Gaer2", "Gears", "Key1", "Key2", "Key", "Revolver" };
    string[] UnityRoomItems = { "Revolver" };
    string[] L2_1Items = { "" };
    string[] L2_2Items = { "" };
    

    // Start is called before the first frame update
    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        if (SceneManager.GetActiveScene().name != "MainMenu")
            PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
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
                checkPlayerPosition();
                break;
            case "L2_1":
                checkItems(L2_1Items);
                checkPlayerPosition();
                checkKeyPad();
                break;
            case "L2_2":
                checkItems(L2_2Items);
                checkPlayerPosition();
                break;
        }
    }

    public Vector3 StringToVector3(string vector)
    {
        vector = vector.Trim(new char[] { ' ', '(', ')' });
        float[] v = vector.Split(',').Select(n => (float)System.Convert.ToDouble(n)).ToArray<float>();
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
            Collector.GameObjects.Player.transform.position = StringToVector3(PlayerPrefs.GetString("PlayerPosition"));
        //if (PlayerPrefs.HasKey("PlayerRotation"))
           // Collector.GameObjects.Player.transform.rotation =  new Quaternion(StringToVector3(PlayerPrefs.GetString("PlayerRotation"));
    }

    private void checkKeyPad()
    {
        if (PlayerPrefs.HasKey("KeyPadIsOpen"))
            if (PlayerPrefs.GetString("KeyPadIsOpen") == "Yes")
                Collector.GameObjects.KeyPad.GetComponent<KeypadLock>().isOpen = true;
    }
}
