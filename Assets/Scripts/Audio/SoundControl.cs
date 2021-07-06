using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundControl : MonoBehaviour
{
    public AudioSource mainMenuMusic;
    public AudioSource playMusic;

    [System.Serializable]
    public struct SoundEffect{
        public string sfxName;
        public AudioSource sfxClip;
    };

    public List<SoundEffect> soundEffects;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMusic()
    {

    }

    public void PlayOneStar()
    {
        var sfx = soundEffects.Find(x => x.sfxName == "bintang_1");
        sfx.sfxClip.PlayOneShot(sfx.sfxClip.clip);
    }

    public void PlayTwoStar()
    {
        var sfx = soundEffects.Find(x => x.sfxName == "bintang_2");
        sfx.sfxClip.PlayOneShot(sfx.sfxClip.clip);
    }

    public void PlayThreeStar()
    {
        var sfx = soundEffects.Find(x => x.sfxName == "bintang_3");
        sfx.sfxClip.PlayOneShot(sfx.sfxClip.clip);
    }

    public void PlayCorrect()
    {
        var sfx = soundEffects.Find(x => x.sfxName == "correct");
        sfx.sfxClip.PlayOneShot(sfx.sfxClip.clip);
    }

    public void PlayWrong()
    {
        var sfx = soundEffects.Find(x => x.sfxName == "wrong");
        sfx.sfxClip.PlayOneShot(sfx.sfxClip.clip);
    }

    public void PlayDoorOpen()
    {
        var sfx = soundEffects.Find(x => x.sfxName == "door_open");
        sfx.sfxClip.PlayOneShot(sfx.sfxClip.clip);
    }
}
