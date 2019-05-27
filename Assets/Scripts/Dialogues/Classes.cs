using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Chat
{
    public Actors[] actors;
    public AllDialogs[] allDialogs;
}

[System.Serializable]
public class Phrase
{
    public int id;
    public int parent;
    public bool isChoice;
    public int actor;
    public int[] participants;
    public string menuText;
    public string dialogueText;
    public int[] outgoingLinks;
    public string[] conditionsString;
    public string[] result;
    public string action;
}

[System.Serializable]
public class Actors
{
    public int id;
    public string name;
    public string in_unity;
}

[System.Serializable]
public class AllDialogs
{
    public string category;
    public СategoryDialogs[] сategoryDialogs;
}

[System.Serializable]
public class СategoryDialogs
{
    public string dg_name;
    public Phrase[] dialogues;
}


public class SinglePhrase
{
    Text board;

    public SinglePhrase(Text board)
    {
        this.board = board;
    }

    public IEnumerator Say(string text, float time)
    {
        board.text = "Марк: " + text;
        yield return new WaitForSeconds(time);
        board.text = "";
    }

    public IEnumerator Say(string name,string text, float time)
    {
        board.text = name + ": " + text;
        yield return new WaitForSeconds(time);
        board.text = "";
    }
}
