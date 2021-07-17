using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    Button btn;
    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GameObject.FindGameObjectWithTag("ImageHolder").GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(renderer);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SetObjectNameAndLoadLevel);
    }

    void SetObjectNameAndLoadLevel()
    {
        PlayerPrefs.SetString("ObjName", gameObject.name);
        renderer.sprite = gameObject.GetComponent<Image>().sprite;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        SoundControl.PlayTouchButton();
    }
}
