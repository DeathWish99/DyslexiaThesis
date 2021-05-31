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
    public Button playButton;
    public Button scoreButton;
    public Button optionsButton;
    public Button quitScore;
    public Button quitOption;
    public Button quitCredits;
    public Button creditsButton;
    

    private void Start()
    {
        playButton.onClick.AddListener(OpenLevelSelect);
        scoreButton.onClick.AddListener(OpenScorePage);
        optionsButton.onClick.AddListener(OpenOptionsPage);
        quitScore.onClick.AddListener(QuitScore);
        quitOption.onClick.AddListener(QuitOption);
        quitCredits.onClick.AddListener(QuitCredits);
        creditsButton.onClick.AddListener(OpenCredits);
    }

    public void OpenLevelSelect()
    {
        Invoke("DelayOpenLevelSelect", 0.3f);
    }

    public void DelayOpenLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenScorePage()
    {
        scorePage.SetActive(true);
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
