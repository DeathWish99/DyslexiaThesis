﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSfxLevel(float sliderValue)
    {
        mixer.SetFloat("Sfx", Mathf.Log10(sliderValue) * 20);
    }


}