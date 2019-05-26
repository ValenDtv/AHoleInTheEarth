using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLevel : MonoBehaviour
{
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = Settings.soundlevel;
    }
    void Update()
    {
        Settings.soundlevel = slider.value;
    }
}
