using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndUpdatePanel : MonoBehaviour
{
    public bool active;

    public void SwitchShowHide()
    {
        gameObject.SetActive(active);
    }
}
