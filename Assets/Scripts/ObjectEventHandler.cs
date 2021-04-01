using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectEventHandler : MonoBehaviour
{
    [SerializeField]private string objName;
    [SerializeField]private GameObject panel;
    [SerializeField]private GameObject gameController;
    public Text uiText;
    private void OnMouseDown()
    {
        panel.GetComponent<OpenAndUpdatePanel>().active = true;
        panel.GetComponent<OpenAndUpdatePanel>().SwitchShowHide();
        panel.GetComponent<OpenAndUpdatePanel>().LoadWord(objName);
        gameController.GetComponent<VoiceController>().currObj = gameObject;
    }


    public void ReplaceContainerWithObject(string speakRes)
    {
        bool spawned = gameController.GetComponent<ItemSpawn>().SpawnItem(speakRes, transform.position);

        if (spawned)
        {
            Destroy(gameObject);
            panel.GetComponent<OpenAndUpdatePanel>().active = false;
            panel.GetComponent<OpenAndUpdatePanel>().SwitchShowHide();
        }
        else
        {
            uiText.text = "Something wrong";
        }
    }
}
