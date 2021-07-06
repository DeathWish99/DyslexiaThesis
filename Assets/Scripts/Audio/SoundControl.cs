using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundControl : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip playMusic;

    [System.Serializable]
    public struct SoundEffect{
        public string sfxName;
        public AudioSource sfxClip;
    };

    public List<SoundEffect> soundEffects;

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
        PlayMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Plays the music designed for the menus
    /// This method is static so that it can be called from anywhere in the code.
    /// </summary>
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

    /// <summary>
    /// Plays the music designed for outside menus
    /// This method is static so that it can be called from anywhere in the code.
    /// </summary>
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
        var sfx = instance.soundEffects.Find(x => x.sfxName == "bintang_1").sfxClip;
        sfx.PlayOneShot(sfx.clip);
    }

    static public void PlayTwoStar()
    {
        var sfx = instance.soundEffects.Find(x => x.sfxName == "bintang_2").sfxClip;
        sfx.PlayOneShot(sfx.clip);
    }

    static public void PlayThreeStar()
    {
        var sfx = instance.soundEffects.Find(x => x.sfxName == "bintang_3").sfxClip;
        sfx.PlayOneShot(sfx.clip);
    }

    static public void PlayCorrect()
    {
        var sfx = instance.soundEffects.Find(x => x.sfxName == "correct").sfxClip;
        sfx.PlayOneShot(sfx.clip);
    }

    static public void PlayWrong()
    {
        var sfx = instance.soundEffects.Find(x => x.sfxName == "wrong").sfxClip;
        sfx.PlayOneShot(sfx.clip);
    }

    static public void PlayDoorOpen()
    {
        var sfx = instance.soundEffects.Find(x => x.sfxName == "door_open").sfxClip;
        sfx.PlayOneShot(sfx.clip);
    }
}
