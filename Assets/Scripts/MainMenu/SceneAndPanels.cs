using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public Button sendCloud;
    public TMP_Text cloudText;


    private void Start()
    {
        playButton.onClick.AddListener(OpenLevelSelect);
        playButton2.onClick.AddListener(OpenLevelSelect);
        scoreButton.onClick.AddListener(OpenScorePage);
        cloudButton.onClick.AddListener(OpenCloudPage);
        optionsButton.onClick.AddListener(OpenOptionsPage);
        quitScore.onClick.AddListener(QuitScore);
        quitCloud.onClick.AddListener(QuitCloud);
        sendCloud.onClick.AddListener(SendDataToServer);
        quitOption.onClick.AddListener(QuitOption);
        quitCredits.onClick.AddListener(QuitCredits);
        creditsButton.onClick.AddListener(OpenCredits);
    }

    private void OpenLevelSelect()
    {
        Maskot.SetActive(false);
        Invoke("DelayOpenLevelSelect", 2f);
    }

    private void DelayOpenLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    private void OpenScorePage()
    {
        scorePage.SetActive(true);
    }
    private void OpenCloudPage()
    {
        cloudText.text = "Kirim data ke server?";
        quitCloud.GetComponentInChildren<TMP_Text>().text = "Tidak";
        sendCloud.gameObject.SetActive(true);
        cloudPage.SetActive(true);
    }
    private void OpenOptionsPage()
    {
        Invoke("DelayOpenOptionsPage", 0.3f);
    }
    private void DelayOpenOptionsPage()
    {
        optionsPage.SetActive(true);
    }
    private void QuitScore()
    {
        Invoke("DelayQuitScore", 0.3f);
    }
    private void DelayQuitScore()
    {
        scorePage.SetActive(false);
    }
    private void QuitCloud()
    {
        Invoke("DelayQuitCloud", 0.3f);
    }
    private void DelayQuitCloud()
    {
        cloudPage.SetActive(false);
    }
    private void QuitOption()
    {
        Invoke("DelayQuitOption", 0.3f);
    }
    private void DelayQuitOption()
    {
        optionsPage.SetActive(false);
    }
    private void OpenCredits()
    {
        creditsPage.SetActive(true);
    }
    private void QuitCredits()
    {
        Invoke("DelayQuitCredits", 0.3f);
    }
    private void DelayQuitCredits()
    {
        creditsPage.SetActive(false);
    }
    private async void SendDataToServer()
    {
        bool test = false;
        if(await DbCommands.InsertUpdateDataToFirebase())
        //if (test)
        {
            cloudText.text = "Terima kasih sudah mengikuti riset kami!";
        }
        else
        {
            cloudText.text = "Gagal mengirim data. Tolong bermain game terlebih dahulu.";
        }
        quitCloud.GetComponentInChildren<TMP_Text>().text = "Tutup";
        sendCloud.gameObject.SetActive(false);

    }
}
