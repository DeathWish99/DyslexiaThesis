using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseDdlBg : MonoBehaviour
{

    public Image ddlBg;
    
    public void ActivateBg()
    {
        ddlBg.gameObject.SetActive(true);
    }

    public void DeactivateBg()
    {
        ddlBg.gameObject.SetActive(false);
    }
}
