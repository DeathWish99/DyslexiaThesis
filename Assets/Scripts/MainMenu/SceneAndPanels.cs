using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneAndPanels : MonoBehaviour
{
    public GameObject scorePage;
    public GameObject optionsPage;
    public GameObject creditsPage;
    public GameObject Maskot;
    public GameObject cloudPage;
    public Button playButton;
    public Button playButton2;
    public Button scoreButton;
    public Button optionsButton;
    public Button quitScore;
    public Button quitOption;
    public Button quitCredits;
    public Button creditsButton;
    public Button cloudButton;
    public Button quitCloud;


    private void Start()
    {
        playButton.onClick.AddListener(OpenLevelSelect);
        playButton2.onClick.AddListener(OpenLevelSelect);
        scoreButton.onClick.AddListener(OpenScorePage);
        cloudButton.onClick.AddListener(OpenCloudPage);
        optionsButton.onClick.AddListener(OpenOptionsPage);
        quitScore.onClick.AddListener(QuitScore);
        quitCloud.onClick.AddListener(QuitCloud);
        quitOption.onClick.AddListener(QuitOption);
        quitCredits.onClick.AddListener(QuitCredits);
        creditsButton.onClick.AddListener(OpenCredits);
    }

    public void OpenLevelSelect()
    {
        Maskot.SetActive(false);
        Invoke("DelayOpenLevelSelect", 2f);
    }

    public void DelayOpenLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenScorePage()
    {
        scorePage.SetActive(true);
    }
    public void OpenCloudPage()
    {
        cloudPage.SetActive(true);
    }
    public void OpenOptionsPage()
    {
        Invoke("DelayOpenOptionsPage", 0.3f);
    }
    public void DelayOpenOptionsPage()
    {
        optionsPage.SetActive(true);
    }
    public void QuitScore()
    {
        Invoke("DelayQuitScore", 0.3f);
    }
    public void DelayQuitScore()
    {
        scorePage.SetActive(false);
    }
    public void QuitCloud()
    {
        Invoke("DelayQuitCloud", 0.3f);
    }
    public void DelayQuitCloud()
    {
        cloudPage.SetActive(false);
    }
    public void QuitOption()
    {
        Invoke("DelayQuitOption", 0.3f);
    }
    public void DelayQuitOption()
    {
        optionsPage.SetActive(false);
    }
    public void OpenCredits()
    {
        creditsPage.SetActive(true);
    }
    public void QuitCredits()
    {
        Invoke("DelayQuitCredits", 0.3f);
    }
    public void DelayQuitCredits()
    {
        creditsPage.SetActive(false);
    }
}
