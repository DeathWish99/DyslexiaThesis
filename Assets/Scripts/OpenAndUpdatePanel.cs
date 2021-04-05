using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndUpdatePanel : LetterTraceClass
{
    public bool active;


    public List<LetterTrace> lettersDict;

    public void SwitchShowHide()
    {
        gameObject.SetActive(active);
    }

    //Loads an array containing letters of the intended word, and sends it to another script which will handle drawing
    public void LoadWord(string receivedWord)
    {
        List<LetterTrace> lettersToSend = new List<LetterTrace>();

        foreach (char letter in receivedWord.ToLower())
        {
            int itemIndex = lettersDict.FindIndex(x => x.letter == letter);
            if (itemIndex > -1)
            {
                lettersToSend.Add(lettersDict[itemIndex]);
            }
            else
            {

            }
        }

        GetComponent<WritingController>().lettersReceived = lettersToSend;
        GetComponent<WritingController>().LoadLettersIntoHierarchy();
    }
}
