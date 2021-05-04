using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SimpleJSON;
using UnityEngine;
using TMPro;

public class ServeGraphsPerWord : MonoBehaviour
{
    public Sprite circleSprite;
    public RectTransform graphContainer;
    public TMP_Dropdown ddlRecords;
    public string targetRecord = "akt"; //temporary.
    private string path;
    private string fromJsonString;
    private JSONNode scoreRecords;
    private List<GameObject> graphObjects;
    private List<UserScore> parsedScoreRecords;
    private UserScore wordToServe;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/ScoreRecords.json";
        fromJsonString = File.ReadAllText(path);
        scoreRecords = JSONNode.Parse(fromJsonString);
        graphObjects = new List<GameObject>();

        parsedScoreRecords = new List<UserScore>();
        ParseRecordsToList();
        AddDropdownOptions();
        CreateGraphByWord();
    }

    void AddDropdownOptions()
    {
        List<string> recordNames = new List<string>();

        foreach(UserScore parsedScoreRecord in parsedScoreRecords)
        {
            recordNames.Add(parsedScoreRecord.objectName);
        }
        ddlRecords.AddOptions(recordNames);
    }

    void ParseRecordsToList()
    {
        for (int i = 0; i < scoreRecords.Count; i++)// (JSONNode node in scoreRecords)
        {
            parsedScoreRecords.Add(new UserScore(scoreRecords[i]["objectName"], new List<float>()));

            foreach (JSONNode score in scoreRecords[i]["scores"])
            {
                parsedScoreRecords[i].scores.Add(score.AsFloat);
            }
        }
    }
    
    public void CreateGraphByWord()
    {
        if(graphObjects.Count > 0)
        {
            foreach (GameObject graphObject in graphObjects)
            {
                Destroy(graphObject);
            }
        }
        string selectedRecord = ddlRecords.GetComponentInChildren<TMP_Text>().text;

        wordToServe = parsedScoreRecords.Where(x => x.objectName == selectedRecord.Trim()).SingleOrDefault();
        graphObjects = CreateGraph.ShowGraph(wordToServe.scores, graphContainer, circleSprite);
        Debug.Log(wordToServe.objectName + " " + wordToServe.scores[0]);
    }
}
