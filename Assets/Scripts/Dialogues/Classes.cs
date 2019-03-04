using UnityEngine;
using System.Collections;

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
