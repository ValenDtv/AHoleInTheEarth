using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjects
{
    public GameObject ThirdPersonCamera;
    public GameObject Player;
    public GameObject Subtitles;
    public GameObject Dialog_choice;
    public GameObject InventWindow;
    public GameObject Cutscene_dialogue;

    public GameObject[] Options = new GameObject[4];

}


public class GameObjectCollector : MonoBehaviour
{
    public GameObjects GameObjects;
    Chat chat;

    void Start()
    {
        string json = Resources.Load<TextAsset>("dialogues").text;
        chat = JsonUtility.FromJson<Chat>(json);

        //foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Thing"))
        //{
        //    Outline outline = gameObject.AddComponent<Outline>();
        //    outline.OutlineMode = Outline.Mode.OutlineVisible;
        //    outline.OutlineWidth = 1;
        //    outline.OutlineColor = Color.yellow;
        //}
        //foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Interactive"))
        //{
        //    Outline outline = gameObject.AddComponent<Outline>();
        //    outline.OutlineMode = Outline.Mode.OutlineVisible;
        //    outline.OutlineWidth = 1;
        //    outline.OutlineColor = Color.yellow;
        //}
        //foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Character"))
        //{
        //    Outline outline = gameObject.AddComponent<Outline>();
        //    outline.OutlineMode = Outline.Mode.OutlineVisible;
        //    outline.OutlineWidth = 1;
        //    outline.OutlineColor = Color.yellow;
        //}
    }

    public СategoryDialogs[] GetСategoryDialogs(string category)
    {
        foreach (AllDialogs ad in chat.allDialogs)
            if (ad.category == category)
                return ad.сategoryDialogs;
        return null;
    }

    public Actors[] GetActors()
    {
        return chat.actors;
    }
}
