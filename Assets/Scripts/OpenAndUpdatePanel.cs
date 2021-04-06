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

    //Loads an array containing letters of the intended word, and loads it into hierarchy
    public void LoadWord(string receivedWord)
    {
        List<LetterTrace> lettersToSpawn = new List<LetterTrace>();

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

        foreach (LetterTrace letterTrace in lettersToSpawn)
        {
            GameObject instance = Instantiate(letterTrace.letterObj, gameObject.transform, false);

            if (letterTrace.letter != lettersToSpawn[0].letter)
            {
                instance.SetActive(false);
            }
        }
        
        GetComponent<DrawLine>().currLetter = lettersToSpawn[0];
        GetComponent<DrawLine>().currIndex = 0;
    }
}
