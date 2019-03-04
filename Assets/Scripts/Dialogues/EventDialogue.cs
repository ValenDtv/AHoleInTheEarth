using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EventDialogue_options
{
    public GameObject Collector;
    public float Time;
    public List<TimeEvent> TimeEvents = new List<TimeEvent>();
    public List<TriggerEvent> TriggerEvents = new List<TriggerEvent>();
}

[System.Serializable]
public class TimeEvent
{
    public bool Active;
    public float Time;
    public string DialogueName;
    public bool Repeat;
    public float TimePassed = 0f;
}

[System.Serializable]
public class TriggerEvent
{
    public GameObject TriggerObject;
    public string DialogueName;
    public bool Repeat;
    private TriggerScript TriggerScript;

    public void SetTriggerScript(TriggerScript triggerScript)
    {
        this.TriggerScript = triggerScript;
    }

    public bool CheckTrigger()
    {
        return TriggerScript.Activated;
    }

    public void TurnOffTrigger()
    {
        TriggerScript.Activated = false;
    }

}

public class EventDialogue : MonoBehaviour
{
    public EventDialogue_options options;
    private GameObjectCollector collector;
    private float time;
    private Text board;
    private List<TimeEvent> TimeEvents;
    private List<TriggerEvent> TriggerEvents;
    Actors[] actors;


    // Start is called before the first frame update
    void Start()
    {
        time = options.Time;
        collector = options.Collector.GetComponent<GameObjectCollector>();
        board = collector.GameObjects.Subtitles.GetComponent<Text>();
        TimeEvents = options.TimeEvents;
        TriggerEvents = options.TriggerEvents;

        foreach (TriggerEvent tre in TriggerEvents)
            tre.SetTriggerScript(tre.TriggerObject.GetComponent<TriggerScript>());
        actors = collector.GetActors();
    }

    // Update is called once per frame
    void Update()
    {
        TimeEvent DoneTimeEvent = null;
        TriggerEvent DoneTriggerEvent = null;

        foreach (TimeEvent timeEvent in TimeEvents)
        {
            if (!timeEvent.Active)
                continue;
            if (timeEvent.DialogueName =="")
                continue;
            timeEvent.TimePassed += Time.deltaTime;
            if (timeEvent.TimePassed >= timeEvent.Time)
            {
                StartCoroutine(Start_dialog(timeEvent.DialogueName));
                if (!timeEvent.Repeat)
                    DoneTimeEvent = timeEvent;
                else
                    timeEvent.TimePassed = 0;
            }
        }
        foreach (TriggerEvent triggerEvent in TriggerEvents)
        {
            if (triggerEvent.DialogueName == "" || triggerEvent.TriggerObject == null)
                continue;
            if (triggerEvent.CheckTrigger())
            {
                StartCoroutine(Start_dialog(triggerEvent.DialogueName));
                if (!triggerEvent.Repeat)
                {
                    DoneTriggerEvent = triggerEvent;
                    Destroy(triggerEvent.TriggerObject);
                }
                else
                    triggerEvent.TurnOffTrigger();
            }
        }
        if (DoneTimeEvent != null)
            TimeEvents.Remove(DoneTimeEvent);
        if (DoneTriggerEvent != null)
            TriggerEvents.Remove(DoneTriggerEvent);

    }

    Phrase[] Find_dialog(СategoryDialogs[] cd, string name)
    {
        for (int i = 0; i < cd.Length; i++)
        {
            if (cd[i].dg_name == name)
                return cd[i].dialogues;
        }
        return null;
    }

    int Find_Phrase(Phrase[] ph, int? ph_id)
    {
        int i = 0;
        while (ph[i].id != ph_id)
            i++;
        return i;
    }

    private string GetActorName(int num)
    {
        foreach (Actors actor in actors)
            if (actor.id == num)
                return actor.name;
        return "";
    }

    public IEnumerator Start_dialog(string dialogue_name)
    {
        Phrase[] ph = Find_dialog(collector.GetСategoryDialogs("event dialogue"), dialogue_name);
        if (ph == null)
            yield break;
        int i = 0;
        int? point = ph[0].id;
        while (true)
        {
            board.text = GetActorName(ph[i].actor)+": "+ph[i].dialogueText;
            yield return new WaitForSeconds(time);
            //Если поле выходных ссылок пустое, значит это последняя фраза в диалоге
            if (ph[i].outgoingLinks.Length == 0)
                break;
            //Если номер выходной ссылки меньше номера текущей фразы, значит это последняя фраза кольцевого диалога
            if (ph[i].outgoingLinks[0] < ph[i].id)
                    break;
            i++;
        }
        board.text = "";
    }
}
