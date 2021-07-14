using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAudioSliderOnSceneChange : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        musicSlider.gameObject.SetActive(true);
        sfxSlider.gameObject.SetActive(true);
        musicSlider.value = musicSlider.GetComponent<GetSetVolume>().GetMusicLevel();
        sfxSlider.value = sfxSlider.GetComponent<GetSetVolume>().GetSfxLevel();
    }
}
