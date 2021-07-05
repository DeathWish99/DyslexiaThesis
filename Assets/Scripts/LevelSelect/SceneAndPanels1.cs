﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneAndPanels1 : MonoBehaviour
{
    public GameObject optionsPage;
    public GameObject creditsPage;
    public Button optionsButton;
    public Button quitOption;
    public Button backButton;
    public Button quitCredits;
    public Button creditsButton;
    

    private void Start()
    {
        optionsButton.onClick.AddListener(OpenOptionsPage);
        backButton.onClick.AddListener(Back);
        quitOption.onClick.AddListener(QuitOption);
        quitCredits.onClick.AddListener(QuitCredits);
        creditsButton.onClick.AddListener(OpenCredits);
    }

    

    public void OpenOptionsPage()
    {
        Invoke("DelayOpenOptionsPage", 0.3f);
    }
    public void DelayOpenOptionsPage()
    {
        optionsPage.SetActive(true);
    }
    public void Back()
    {
        Invoke("DelayBack", 0.3f);
    }
    public void DelayBack()
    {
        SceneManager.LoadScene(0);
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
