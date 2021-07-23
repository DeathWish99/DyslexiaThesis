using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    public Animator an;
    public bool playButton;
    void Start()
    {
        if(!playButton)
        an = GetComponent<Animator>();
    }

    public void OnClick()
    {
        an.SetTrigger("Click");
        SoundControl.PlayTouchButton();
    }

    public void OnClickDoor()
    {
        an.SetTrigger("Click");
        SoundControl.PlayDoorOpen();
    }

    public void OnClickToMainMenu()
    {
        an.SetTrigger("Click");
        SoundControl.PlayTouchButton();
        SceneManager.LoadScene(0);
    }
}
