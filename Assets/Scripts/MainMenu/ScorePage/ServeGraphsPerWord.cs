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
    //private string path;
    private string fromJsonString;
    private bool firstOpen;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private List<RectTransform> rectXs;
    private JSONNode scoreRecords;
    private List<GameObject> graphObjects;
    private List<UserScore> parsedScoreRecords;
    private UserScore wordToServe;
    // Start is called before the first frame update
    void Start()
    {
        //path = Application.dataPath + "/ScoreRecords.json";
        fromJsonString = DbCommands.GetScoresJson();
        scoreRecords = JSONNode.Parse(fromJsonString);
        Debug.Log(fromJsonString);
        Debug.Log(scoreRecords);
        graphObjects = new List<GameObject>();
        dashTemplateX = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
        firstOpen = false;
        rectXs = new List<RectTransform>();
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
            graphObjects.Clear();
        }
        if(rectXs.Count > 0)
        {
            foreach(RectTransform rectX in rectXs)
            {
                Destroy(rectX.gameObject);
            }
            rectXs.Clear();
        }
        string selectedRecord = ddlRecords.GetComponentInChildren<TMP_Text>().text;

        wordToServe = parsedScoreRecords.Where(x => x.objectName == selectedRecord.Trim()).SingleOrDefault();
        graphObjects = CreateGraph.ShowGraph(wordToServe.scores, graphContainer, circleSprite);

        List<GameObject> points = graphObjects.Where(x => x.name == "circle").ToList();

        for(int i = 0; i < points.Count; i++)
        {
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(points[i].GetComponent<RectTransform>().anchoredPosition.x, -7f);
            labelX.GetComponent<TMP_Text>().text = (i + 1).ToString();
            rectXs.Add(labelX);

            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(points[i].GetComponent<RectTransform>().anchoredPosition.x, 0f);
            dashX.sizeDelta = new Vector2(graphContainer.sizeDelta.y, 3);
            dashX.localScale = new Vector3(1f, 0.3f, 0.3f);
            rectXs.Add(dashX);

        }

        if (!firstOpen)
        {
            for (int i = 0; i < 11; i++)
            {
                RectTransform labelY = Instantiate(labelTemplateY);
                labelY.SetParent(graphContainer);
                labelY.gameObject.SetActive(true);
                float normalizedValue = i * 1f / 10;
                labelY.anchoredPosition = new Vector2(-12f, normalizedValue * graphContainer.sizeDelta.y);
                labelY.GetComponent<TMP_Text>().text = Mathf.RoundToInt(normalizedValue * 100f).ToString();

                RectTransform dashY = Instantiate(dashTemplateY);
                dashY.SetParent(graphContainer);
                dashY.gameObject.SetActive(true);
                dashY.sizeDelta = new Vector2(graphContainer.sizeDelta.x, 3);
                dashY.localScale = new Vector3(1f, 0.3f, 0.3f);
                dashY.anchoredPosition = new Vector2(0, normalizedValue * graphContainer.sizeDelta.y);
            }
            firstOpen = true;

        }

        Debug.Log(wordToServe.objectName + " " + wordToServe.scores[0]);
    }
}
