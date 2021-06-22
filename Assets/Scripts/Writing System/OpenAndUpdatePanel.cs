﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndUpdatePanel : LetterTraceClass
{
    public bool active;
    public List<LetterTrace> lettersDict;
    public GameObject gameController;

    private List<LetterTrace> lettersToSpawn;

    private void Start()
    {
        LoadWord(PlayerPrefs.GetString("ObjName"));
    }

    public void SwitchShowHide()
    {
        gameObject.SetActive(active);
    }

    //Loads an array containing letters of the intended word, and loads it into hierarchy
    public void LoadWord(string receivedWord)
    {
        lettersToSpawn = new List<LetterTrace>();
        foreach (char letter in receivedWord.ToLower())
        {
            int itemIndex = lettersDict.FindIndex(x => x.letter == letter);
            if (itemIndex > -1)
            {
                lettersToSpawn.Add(lettersDict[itemIndex]);
            }
            else
            {

            }
        }
        
        GameObject instance = Instantiate(lettersToSpawn[0].letterObj, gameObject.transform, false);
        instance.SetActive(true);
        instance.tag = "Current Letter";

        gameController.GetComponent<VoiceController>().currObjName = receivedWord.ToLower();


        GetComponent<DrawLine>().currWord = lettersToSpawn;
        GetComponent<DrawLine>().currIndex = 0;
    }

    public void ShowSuccessScreen(string result)
    {

    }
}
