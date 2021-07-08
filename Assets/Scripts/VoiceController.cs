using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "id-ID";

    //Object that is to be spawned
    public string currObjName;
    public float score;
    [SerializeField] Text uiText;

    private void Start()
    {
        Setup(LANG_CODE);
#if UNITY_ANDROID
        Debug.Log("Android");
#elif UNITY_EDITOR
        Debug.Log("Editor");
#endif


#if UNITY_ANDROID
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif

        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        //TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        //TextToSpeech.instance.onDoneCallback = OnSpeakStop;
        CheckPermission(); 
    }

    void CheckPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    //public void StartSpeaking(string message)
    //{
    //    TextToSpeech.instance.StartSpeak(message);
    //}

    //public void StopSpeaking()
    //{
    //    TextToSpeech.instance.StopSpeak();
    //}

    //void OnSpeakStart()
    //{
    //    Debug.Log("Speak Start");
    //}

    //void OnSpeakStop()
    //{
    //    Debug.Log("Speak Stop");
    //}

    public void StartListening()
    {
        SpeechToText.instance.StartRecording("Start Speaking");
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result = "")
    {
        uiText.text = result;
        if (currObjName.Equals(result.ToLower()))
        {
            PlayerPrefs.SetString("ObjectResult", result);
            SceneManager.LoadScene(3);
            StopListening();
        }
        else
        {
            //Play voice over, try again
        }
    }

    void OnPartialSpeechResult(string result = "")
    {
        uiText.text = result;
        if (currObjName.Equals(result.ToLower()))
        {
            PlayerPrefs.SetString("ObjectResult", result);
            SceneManager.LoadScene(3);
            StopListening();
        }
        else
        {
            //Play voice over, try again
        }
    }
    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }
}
