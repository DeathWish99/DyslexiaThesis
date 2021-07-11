using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip playMusic;

    [System.Serializable]
    public struct SoundEffect{
        public string sfxName;
        public AudioClip sfxClip;
    };

    public List<SoundEffect> soundEffects;
    public List<SoundEffect> letterSoundEffects;
    public List<SoundEffect> wordSoundEffects;

    public Slider musicSlider;
    public Slider sfxSlider;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    static private SoundControl instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            // Register as singleton if first
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        musicSlider.value = musicSource.volume;
        sfxSlider.value = sfxSource.volume;
        PlayMenuMusic();
    }
    

    static public void PlayMenuMusic()
    {
        if (instance != null)
        {
            if (instance.musicSource != null)
            {
                instance.musicSource.Stop();
            }
            Debug.Log(instance.musicSource);
            instance.musicSource.clip = instance.mainMenuMusic;
            instance.musicSource.Play();
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
    
    static public void PlayGameMusic()
    {
        if (instance != null)
        {
            if (instance.musicSource != null)
            {
                instance.musicSource.Stop();
            }
            instance.musicSource.clip = instance.playMusic;
            instance.musicSource.Play();
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayOneStar()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "bintang_1").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayTwoStar()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "bintang_2").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayThreeStar()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "bintang_3").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayCorrect()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "correct").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayCorrectVoice()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "correct_voice").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayWrong()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "wrong").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayDoorOpen()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "door_open").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayTouchButton()
    {
        if (instance != null)
        {
            var sfx = instance.soundEffects.Find(x => x.sfxName == "touch_button").sfxClip;
            if (instance.sfxSource != null)
            {
                instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayLetterSound(char letter)
    {
        if (instance != null)
        {
            var sfx = instance.letterSoundEffects.Find(x => Convert.ToChar(x.sfxName) == letter).sfxClip;
            if (instance.sfxSource != null)
            {
                //instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayWordSound(string word)
    {
        if (instance != null)
        {
            var sfx = instance.letterSoundEffects.Find(x => x.sfxName == word).sfxClip;
            if (instance.sfxSource != null)
            {
                //instance.sfxSource.Stop();
            }
            instance.sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
}
