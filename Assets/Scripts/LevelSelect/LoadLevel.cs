using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SetObjectNameAndLoadLevel);
    }

    void SetObjectNameAndLoadLevel()
    {
        PlayerPrefs.SetString("ObjName", gameObject.name);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
