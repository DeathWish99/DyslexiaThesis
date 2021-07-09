using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SoundControl.PlayDoorOpen();
    }
}
