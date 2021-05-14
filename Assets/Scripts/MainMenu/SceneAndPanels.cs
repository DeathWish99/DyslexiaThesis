using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneAndPanels : MonoBehaviour
{
    public GameObject scorePage;
    public GameObject optionsPage;
    public Button playButton;
    public Button scoreButton;
    public Button optionsButton;
    public Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(OpenLevelSelect);
        scoreButton.onClick.AddListener(OpenScorePage);
        optionsButton.onClick.AddListener(OpenOptionsPage);
        quitButton.onClick.AddListener(QuitGame);
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
        optionsPage.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
