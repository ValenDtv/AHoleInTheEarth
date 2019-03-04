using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Invector.CharacterController;


[System.Serializable]
 public class Dialogue_options
{
    public GameObject Collector;
    public float Time;
    public int Point;
    public float Distance;
}


public class InteractiveDialogue : MonoBehaviour
{
    private class Char_transform
    {
        public GameObject character;
        public Vector3 position_org;
        public Quaternion rotation_org;

        public Char_transform(GameObject character)
        {
            this.character = character;
            this.position_org = character.transform.position;
            this.rotation_org = character.transform.rotation;
        }
    }

    public Dialogue_options dialogue_Options;
    private GameObjectCollector collector;
    private float time;
    private int? point;
    private Text board;
    private Text[] Options = new Text[4];
    private Dictionary<int, GameObject> cameras;
    private Dictionary<int, Char_transform> characters;
    private GameObject main_camera;
    private GameObject dialog_choice;
    private Transform p_transform;
    private vThirdPersonInput fpi;
    private float distance;
    private bool MouseButtonPressed = false;
    Actors[] actors;
    CursorLockMode wantedMode;


    void Start()
    {
        time = dialogue_Options.Time;
        distance = dialogue_Options.Distance;
        point = dialogue_Options.Point;
        collector = dialogue_Options.Collector.GetComponent<GameObjectCollector>();
        board = collector.GameObjects.Subtitles.GetComponent<Text>();
        actors = collector.GetActors();
        p_transform = this.gameObject.transform;
        main_camera = collector.GameObjects.ThirdPersonCamera;
        fpi = collector.GameObjects.Player.GetComponent<vThirdPersonInput>();
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
        for (int j=0; j < ph[i].conditionsString.Length; j++)
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

    public IEnumerator Start_dialog()
    {
        if (point == null)
            yield break;
        Phrase[] ph = Find_dialog(collector.GetСategoryDialogs("interactive dialogue"), gameObject.name);
        if (ph == null)
            yield break;
        int i = Find_Phrase(ph, point);
        fpi.disable = true;
        yield return new WaitForSeconds(0.2f);
        Dictionary_initialization(ph[i].participants);
        Character_initialization(actors);
        Camera_initialization(actors);

        Cursor.lockState = wantedMode = CursorLockMode.Confined;
        Cursor.visible = true;
        while (true)
        {
            Turn_the_camera(ph[i]);
            if (ph[i].isChoice)
            {
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
                continue;
            }
            board.text = GetActorName(ph[i].actor) + ": " + ph[i].dialogueText;
            yield return StartCoroutine(WaitEndOfPhrase());
            //Если поле выходных ссылок пустое, значит это последняя фраза в диалоге
            if (ph[i].outgoingLinks.Length == 0)
            {
                if (i < ph.Length-1)
                point = ph?[i + 1].id;
                break;
            }
            //Если значение выходной ссылки меньше номера текущей фразы, значит это последняя фраза кольцевого диалога
            if (ph[i].outgoingLinks[0] < ph[i].id)
            {
                point = ph[i].outgoingLinks[0];
                i = Find_Phrase(ph, point);
                if (ph[i].isChoice)
                    continue;
                else
                    break;
            }
            if (ph[i].outgoingLinks[0] != ph[i + 1].id)
            {
                i = Find_Phrase(ph, ph[i].outgoingLinks[0]);
                continue;
            }
            i++;
        }
        board.text = "";
        Camera_finalization();
        Character_finalization();
        fpi.disable = false;
        Cursor.lockState = wantedMode = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Изменение активной камеры
    void Turn_the_camera(Phrase ph)
    {
        if (ph.isChoice)
            cameras[ph.actor].SetActive(true);
        foreach (GameObject camera in cameras.Values)
            camera.SetActive(false);
        cameras[ph.actor].SetActive(true);
        return;
    }

    //Создание словаря с камерами
    void Dictionary_initialization(int[] participants)
    {
        cameras = new Dictionary<int, GameObject>();
        for (int i = 0; i < participants.Length; i++)
            cameras.Add(participants[i], null);
        characters = new Dictionary<int, Char_transform>();
    }

    //Формирование камер для персонажей
    void Camera_initialization(Actors[] actors)
    {
        foreach (int ch_num in new List<int>(cameras.Keys))
        {
            GameObject camera = new GameObject();
            camera.SetActive(false);
            camera.AddComponent<Camera>();
            float y = (p_transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
            float center_x = p_transform.position.x + distance / 2 * Mathf.Sin(y);
            float center_z = p_transform.position.z + distance / 2 * Mathf.Cos(y);
            Vector3 vector = new Vector3(center_x + (characters[ch_num].character.transform.position.x - center_x) * 0.4f,
                    characters[ch_num].character.transform.position.y + 1.5f,
                    center_z + (characters[ch_num].character.transform.position.z - center_z) * 0.4f);
            camera.transform.position = vector;
            camera.transform.LookAt(GameObject.Find(characters[ch_num].character.name + ":Head").transform);
            //Заполнение словаря с камерами
            cameras[ch_num] = camera;
        }
        main_camera.SetActive(false);
        return;
    }

    //Расстановка персонажей
    void Character_initialization(Actors[] actors)
    {
        int angle = 0;
        int delta = 0;
        float y = p_transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float center_x = p_transform.position.x + distance / 2 * Mathf.Sin(y);
        float center_z = p_transform.position.z + distance / 2 * Mathf.Cos(y);
        foreach (int ch_num in cameras.Keys)
        {
            string name = "";
            foreach (Actors a in actors)
                if (ch_num == a.id)
                    name = a.in_unity;
            if (name != gameObject.name)
            {
                int final_angle = 180 + angle;
                GameObject character = GameObject.Find(name);
                characters.Add(ch_num, new Char_transform(character));
                character.transform.Rotate(new Vector3(0, 360 - character.transform.rotation.eulerAngles.y
                + (final_angle + p_transform.rotation.eulerAngles.y), 0));
                character.transform.position = new Vector3(center_x + distance / 2 * Mathf.Sin(y + angle * Mathf.Deg2Rad),
                    character.transform.position.y, center_z + distance / 2 * Mathf.Cos(y + angle * Mathf.Deg2Rad));
                delta += 90;
                angle += delta;
            }
            else
            {
                characters.Add(ch_num, new Char_transform(this.gameObject));
            }

        }
        return;
    }

    //Отключение созданных камер
    void Camera_finalization()
    {
        foreach (GameObject camera in cameras.Values)
            Destroy(camera);
        main_camera.SetActive(true);
        return;
    }

    //Возвращение персонажей на их места
    void Character_finalization()
    {
        foreach (int ch_num in characters.Keys)
            if (characters[ch_num].character != this.gameObject)
            {
                characters[ch_num].character.transform.position = characters[ch_num].position_org;
                characters[ch_num].character.transform.rotation = characters[ch_num].rotation_org;
            }
    }

    //Ожидание, когда закончится фраза или когда игрок нажмёт кнопку пропуска фраз диалога
    private IEnumerator WaitEndOfPhrase()
    {
        float time_passed = 0f;
        while (true)
        {
            yield return null;
            time_passed += Time.deltaTime;
            if (MouseButtonPressed || time_passed >= time) break;
        }
    }
}