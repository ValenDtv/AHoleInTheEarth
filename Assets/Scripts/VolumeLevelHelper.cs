using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeLevelHelper : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        source.volume = Settings.soundlevel;
    }
}
