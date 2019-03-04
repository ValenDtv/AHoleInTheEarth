using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CommentDialogue_options
{
    public GameObject Collector;
    public float Time;
}

public class CommentDialogue : MonoBehaviour
{
    public CommentDialogue_options options;
    private GameObjectCollector collector;
    private float time;
    private Text board;
    Actors[] actors;

    // Start is called before the first frame update
    void Start()
    {
        time = options.Time;
        collector = options.Collector.GetComponent<GameObjectCollector>();
        board = collector.GameObjects.Subtitles.GetComponent<Text>();
        actors = collector.GetActors();
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

    public IEnumerator Start_dialog()
    {
        Phrase[] ph = Find_dialog(collector.GetСategoryDialogs("comment dialogue"), gameObject.name);
        if (ph == null)
            yield break;
        int i = 0;
        int? point = ph[0].id;
        while (true)
        {
            board.text = GetActorName(ph[i].actor) + ": " + ph[i].dialogueText;
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

