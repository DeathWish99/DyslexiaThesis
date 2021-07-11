using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using static LetterTraceClass;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "id-ID";

    //Object that is to be spawned
    public string currObjName;
    public List<LetterTrace> currLetterObjs;
    public float score;

    public GameObject gamePanel;
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
        //SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
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

        var spokenLetter = currLetterObjs.Find(x => x.letterName.ToString() == result).letterName.ToString();

        Debug.Log(spokenLetter);
        if (currObjName.Equals(result.ToLower()))
        {
            PlayerPrefs.SetString("ObjectResult", result);
            SceneManager.LoadScene(3);
            StopListening();
        }
        else
        {
            //Play voice over, try again
            SoundControl.PlayWrong();
            StopListening();
        }
    }

    void OnPartialSpeechResult(string result = "")
    {
        uiText.text = result;

        var spokenLetter = currLetterObjs.Find(x => x.letterName.ToString() == result).letterName.ToString();
        Debug.Log(spokenLetter);
        if (currObjName.Equals(result.ToLower()))
        {
            PlayerPrefs.SetString("ObjectResult", result);
            SceneManager.LoadScene(3);
            StopListening();
        }
        else
        {
            //Play voice over, try again
            SoundControl.PlayWrong();
            StopListening();
        }
    }
    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }
}
