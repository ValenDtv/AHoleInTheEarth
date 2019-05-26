using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


[System.Serializable]
public class CutsceneDialogue_options
{
    public GameObject Collector;
    //public float Time;
    //public int Point;
    //public float Distance;
}


public class CutsceneDialogue : MonoBehaviour
{
    public CutsceneDialogue_options dialogue_Options;
    public string Cutscene_name;
    private GameObjectCollector collector;
    public int? point;
    private Text board;
    private Text[] Options = new Text[4];
    private GameObject dialog_choice;
    private bool MouseButtonPressed = false;
    Actors[] actors;
    CursorLockMode wantedMode;

    public PlayableDirector playableDirector;

    // Start is called before the first frame update
    void Start()
    {
        
        point = 10;
        collector = dialogue_Options.Collector.GetComponent<GameObjectCollector>();
        board = collector.GameObjects.Subtitles.GetComponent<Text>();
        board.text = "";
        actors = collector.GetActors();
        dialog_choice = collector.GameObjects.Dialog_choice;
        for (int i = 0; i < 4; i++)
            Options[i] = collector.GameObjects.Options[i].GetComponentInChildren<Text>();
    }

    void Update()
    {
        MouseButtonPressed = Input.GetKeyDown(KeyCode.Mouse0);
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

    IEnumerator Choice(Phrase[] ph, int i)
    {
        for (int j = 0; j < ph[i].outgoingLinks.Length; j++)
        {
            Options[j].text = ph[Find_Phrase(ph, ph[i].outgoingLinks[j])].menuText;
            collector.GameObjects.Options[j].SetActive(true);
        }
        dialog_choice.SetActive(true);
        while (true)
        {
            for (int j = 0; j < ph[i].outgoingLinks.Length; j++)
            {
                if (Options[j].GetComponentInParent<Option>().is_clicked || Input.GetKeyDown((j + 1).ToString()))
                {
                    point = ph[i].outgoingLinks[j];
                    for (int k = 0; k < ph[i].outgoingLinks.Length; k++)
                    {
                        Options[k].text = "";
                        Options[k].GetComponentInParent<Option>().is_clicked = false;
                    }
                    dialog_choice.SetActive(false);
                    yield break;
                }
            }
            yield return null;
        }
    }

    IEnumerator Fork(Phrase[] ph, int i)
    {
        for (int j = 0; j < ph[i].conditionsString.Length; j++)
        {

            string[] split_cond = ph[i].conditionsString[j].Split(new char[] { ':' });
            if (PlayerPrefs.HasKey(split_cond[0]))
                if (PlayerPrefs.GetString(split_cond[0]) == split_cond[1])
                {
                    point = ph[i].outgoingLinks[j];
                    yield break;
                }
        }
        yield break;
    }

    private string GetActorName(int num)
    {
        foreach (Actors actor in actors)
            if (actor.id == num)
                return actor.name;
        return "";
    }

    //Ожидание, когда закончится фраза или когда игрок нажмёт кнопку пропуска фраз диалога
    private IEnumerator WaitEndOfPhrase(float time)
    {
        float time_passed = 0f;
        while (true)
        {
            yield return null;
            time_passed += Time.deltaTime;
            if (MouseButtonPressed || time_passed >= time) break;
        }
    }

    public IEnumerator Next_speech(float time)
    {
        if (point == null)
            yield break;
        Phrase[] ph = Find_dialog(collector.GetСategoryDialogs("cutscene dialogue"), Cutscene_name);
        if (ph == null)
            yield break;
        int i = Find_Phrase(ph, point);
        Cursor.lockState = wantedMode = CursorLockMode.Confined;
        Cursor.visible = true;
        if (ph[i].isChoice)
        {
            if (playableDirector != null)
                playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);

            if (ph[i].conditionsString.Length == 0)
                yield return StartCoroutine(Choice(ph, i));
            else
                yield return StartCoroutine(Fork(ph, i));
            i = Find_Phrase(ph, point);
            if (ph[i].result.Length != 0)
                foreach (string res in ph[i].result)
                {
                    string[] split_res = res.Split(new char[] { ':' });
                    PlayerPrefs.SetString(split_res[0], split_res[1]);
                }
            //yield return Next_speech(time);
            board.text = "";
            Cursor.lockState = wantedMode = CursorLockMode.Locked;
            Cursor.visible = false;

            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
            yield break;
        }
        board.text = GetActorName(ph[i].actor) + ": " + ph[i].dialogueText;
        yield return StartCoroutine(WaitEndOfPhrase(time));
        //Если поле выходных ссылок пустое, значит это последняя фраза в диалоге
        if (ph[i].outgoingLinks.Length > 0)
            point = ph[i].outgoingLinks[0];
        board.text = "";
        Cursor.lockState = wantedMode = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
