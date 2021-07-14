
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesAndPanelsStageSelect : MonoBehaviour
{
    public Button backButton;
    
    private void Start()
    {
        backButton.onClick.AddListener(Back);
    }
    
   
    public void Back()
    {
        Invoke("DelayBack", 0.3f);
    }
    public void DelayBack()
    {
        SceneManager.LoadScene(0);
    }
}