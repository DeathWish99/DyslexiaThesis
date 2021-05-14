using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    private Animator an;
    void Start()
    {
        an = GetComponent<Animator>();
    }

    public void OnClick()
    {
        an.SetTrigger("Click");
    }
}
