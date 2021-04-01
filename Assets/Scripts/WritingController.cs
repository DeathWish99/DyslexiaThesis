using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingController : LetterTraceClass
{
    public List<LetterTrace> lettersReceived;
    private LetterTrace currLetter;
    private int currIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLettersIntoHierarchy()
    {
        foreach(LetterTrace letterTrace in lettersReceived)
        {
            GameObject instance = Instantiate(letterTrace.letterObj, gameObject.transform, true);

            if(letterTrace.letter != lettersReceived[0].letter)
            {
                instance.SetActive(false);
            }
        }

        currLetter = lettersReceived[0];
        currIndex = 0;
    }
}
