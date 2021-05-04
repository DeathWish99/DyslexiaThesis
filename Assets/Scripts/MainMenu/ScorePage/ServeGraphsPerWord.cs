using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

public class ServeGraphsPerWord : MonoBehaviour
{
    public Sprite circleSprite;
    public RectTransform graphContainer;
    public string targetRecord = "akt"; //temporary.
    private string path;
    private string fromJsonString;
    private JSONNode scoreRecords;
    private UserScore wordToServe;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/ScoreRecords.json";
        fromJsonString = File.ReadAllText(path);
        scoreRecords = JSONNode.Parse(fromJsonString);
        CreateGraphByWord();
    }
    
    void CreateGraphByWord()
    {
        foreach(JSONNode node in scoreRecords)
        {
            if(node["objectName"] == targetRecord)
            {
                wordToServe = new UserScore(node["objectName"], new List<float>());

                foreach(JSONNode score in node["scores"])
                {
                    wordToServe.scores.Add(score.AsFloat);
                }
                break;
            }
        }
        CreateGraph.ShowGraph(wordToServe.scores, graphContainer, circleSprite);
        Debug.Log(wordToServe.objectName + " " + wordToServe.scores[0]);
    }

}

[System.Serializable]
public class UserScore
{
    public string objectName;
    public List<float> scores;

    public UserScore(string objectName, List<float> scores)
    {
        this.objectName = objectName;
        this.scores = scores;
    }
}
