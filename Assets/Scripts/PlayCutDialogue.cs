using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutDialogue : MonoBehaviour
{
    private CutsceneDialogue cd;
    // Start is called before the first frame update
    void Start()
    {
        cd = this.GetComponent<CutsceneDialogue>();
        cd.Cutscene_name = "Первая катсцена";
        cd.point = 10;
        StartCoroutine(PlayD());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayD()
    {
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
        yield break;
    }
}
