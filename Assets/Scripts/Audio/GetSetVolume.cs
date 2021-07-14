using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetSetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicSlider", sliderValue);
    }
    public void SetSfxLevel(float sliderValue)
    {
        mixer.SetFloat("Sfx", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SfxSlider", sliderValue);
    }

    public float GetMusicLevel()
    {
        Debug.Log(PlayerPrefs.GetFloat("MusicSlider"));
        return PlayerPrefs.GetFloat("MusicSlider");
    }

    public float GetSfxLevel()
    {

        Debug.Log(PlayerPrefs.GetFloat("SfxSlider"));
        return PlayerPrefs.GetFloat("SfxSlider");

    }

}
