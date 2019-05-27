using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCutDialogue : MonoBehaviour
{
    private CutsceneDialogue cd;
    public string levelName = "L2_1";
    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        cd = this.GetComponent<CutsceneDialogue>();
        cd.Cutscene_name = "Первая катсцена";
        cd.point = 10;
        StartCoroutine(PlayD());

    }
    
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadAsync()
    {
        async = SceneManager.LoadSceneAsync(levelName);
        yield return true;
        async.allowSceneActivation = false;
    }

    private IEnumerator PlayD()
    {
        yield return new WaitForSeconds(32);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        yield return cd.Next_speech(3);
        //LoadAsync();
        yield return new WaitForSeconds(120);
        Application.LoadLevel(levelName);
        yield break;
    }
}
