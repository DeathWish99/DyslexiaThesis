using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
public class DrawLine : LetterTraceClass
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public Camera uiCam;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> fingerPositions;

    public List<LetterTrace> currWord;

    public Text shownWord;
    LetterTrace tempLetter;
    public int currIndex;
    private float wordScore;

    private string path;

    [SerializeField] private GameObject speakButton;
    private List<LinesCondition> linesInLetter = null;
    private void Start()
    {
        speakButton.SetActive(false);
        path = Application.dataPath + "/ScoreRecords.json";
        tempLetter = currWord[currIndex];
        shownWord.text = "";
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = uiCam.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
    }

    //Create first point of line when starting to draw
    private void CreateLine()
    {
        currentLine = Instantiate(linePrefab);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(uiCam.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(uiCam.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();
    }

    //Continues creating line as player drags finger across the screen
    private void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }

    private void EndLine()
    {
        uiCam.GetComponent<WritingController>().edgeColliderPoints = edgeCollider.points;
        List<GameObject> lines = uiCam.GetComponent<WritingController>().ShootRayToImage(currentLine);

        if (lines != null)
        {
            //Debug.Log("Collider Center : " + m_Center);
            //Debug.Log("Collider Size : " + m_Size);
            //Debug.Log("Collider bound Minimum : " + m_Min);
            //Debug.Log("Collider bound Maximum : " + m_Max);

            ////Fetch the Collider from the GameObject
            ////Fetch the center of the Collider volume
            //m_Center = edgeCollider.bounds.center;
            ////Fetch the size of the Collider volume
            //m_Size = edgeCollider.bounds.size;
            ////Fetch the minimum and maximum bounds of the Collider volume
            //m_Min = edgeCollider.bounds.min;
            //m_Max = edgeCollider.bounds.max;
            ProcessTraceLines(lines);

            if (CheckDrawn())
            {
                NextLetterOrSpeak();
            }
        }
        else
        {
            //Play voice telling player to try again
        }
    }

    private void ProcessTraceLines(List<GameObject> detectedLines)
    {
        float lineScore;
        bool curved;
        if(linesInLetter == null || linesInLetter.Count == 0)
        {
            GetLinesInLetterObj();
        }

        for(int i = 0; i < linesInLetter.Count; i++)
        {
            LinesCondition temp;
            var detectedLinesList = detectedLines.FindAll(x => x.gameObject.name == linesInLetter[i].lineObj.name);

            temp.lineObj = linesInLetter[i].lineObj;
            temp.tempCount = detectedLinesList.Count;
            temp.drawn = linesInLetter[i].drawn;

            linesInLetter[i] = temp;
        }

        var intendedLine = linesInLetter.OrderByDescending(line => line.tempCount).First();
        var lineIndex = linesInLetter.FindIndex(line => line.lineObj.name == intendedLine.lineObj.name);
        BoxCollider[] boxColliders = intendedLine.lineObj.GetComponents<BoxCollider>();
        try
        {
            Debug.Log(boxColliders.Length);

            if (boxColliders.Length > 0)
                curved = true;
            else
                curved = false;
        }
        catch(Exception e)
        {
            curved = false;
        }

        Debug.Log(curved);
        RectTransform rtLine = (RectTransform)intendedLine.lineObj.transform;

        int maxPoints = Convert.ToInt32(rtLine.localScale.y) / 2;

        //edgeCollider.bounds.size;
        lineScore = ((float)intendedLine.tempCount / (float)edgeCollider.points.Count()) * 100f;

        bool viableLength;

        if (!curved)
        {
            float scale = intendedLine.lineObj.transform.localScale.y;

            if((float)edgeCollider.points.Length * 2 * 1.2 /*Range between 1 and 2 as multiplier for accuracy*/ > scale)
            {
                viableLength = true;
            }
            else
            {
                viableLength = false;
            }
        }
        else
        {
            if(boxColliders.Length > 50)
            {

            }
            viableLength = true;
        }

        if (lineScore > 60 && !intendedLine.drawn && !linesInLetter[lineIndex].Equals(null) && viableLength)
        {
            Debug.Log("Line score: " + lineScore);
            tempLetter.letterScore += lineScore;
            intendedLine.drawn = true;
            linesInLetter[lineIndex] = intendedLine;
        }
        else
        {
            Debug.Log("Nothing exists");
            Destroy(currentLine);
        }
    }

    private void GetLinesInLetterObj()
    {
        linesInLetter = new List<LinesCondition>();
        foreach (Transform child in currWord[currIndex].letterObj.transform)
        {
            if (child.name.Like("%Line%"))
            {
                LinesCondition tempLines = new LinesCondition();
                tempLines.lineObj = child.gameObject;
                tempLines.drawn = false;

                linesInLetter.Add(tempLines);
            }
        }
    }

    private bool CheckDrawn()
    {
        if(linesInLetter.Where(line => line.lineObj.activeSelf).Any(line => !line.drawn))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void NextLetterOrSpeak()
    {
        GameObject currLetter = GameObject.FindGameObjectWithTag("Current Letter");

        GameObject[] drawnLines = GameObject.FindGameObjectsWithTag("Line");

        tempLetter.letterScore /= linesInLetter.Count;
        currWord[currIndex] = tempLetter;
        Debug.Log("Letter Score: " + currWord[currIndex].letterScore);


        foreach (GameObject drawnLine in drawnLines)
        {
            Destroy(drawnLine);
        }
        if (currIndex < currWord.Count - 1)
        {
            shownWord.text += tempLetter.letterName;
            currIndex += 1;
            tempLetter = currWord[currIndex];
            GameObject nextLetter = Instantiate(currWord[currIndex].letterObj, gameObject.transform, false);
            nextLetter.SetActive(true);
            nextLetter.tag = "Current Letter";
            Destroy(currLetter);

            //insert into playerprefs
            linesInLetter = null;
        }
        else
        {
            foreach(LetterTrace letter in currWord)
            {
                wordScore += letter.letterScore;
            }

            wordScore /= currWord.Count;
            
            string word = "";
            for (int i = 0; i < currWord.Count; i++)
            {
                word += currWord[i].letterName.ToString();
            }
            
            List<UserScore> userScores = new List<UserScore>();
            List<float> newScore = new List<float>();
            newScore.Add(wordScore);
            string fromJsonString = "";
            if (File.Exists(path))
            {
                fromJsonString = File.ReadAllText(path);
            }

            if (fromJsonString != "")
            {
                var data = JSONNode.Parse(fromJsonString);
                foreach (JSONNode node in data)
                {
                    userScores.Add(JsonUtility.FromJson<UserScore>(node.ToString()));
                }
                
                bool keyExist = false;

                foreach (UserScore scoreObj in userScores)
                {
                    if (scoreObj.objectName.Equals(word))
                    {
                        scoreObj.scores.Add(wordScore);
                        keyExist = true;
                        break;
                    }
                }

                if (!keyExist)
                {
                    userScores.Add(new UserScore(word, newScore));
                }
            }
            else
            {
                userScores.Add(new UserScore(word, newScore));
            }

            string toJsonString = "{\"Records\":";
            foreach (UserScore userScore in userScores)
            {
                toJsonString += JsonUtility.ToJson(userScore) + ",";
            }
            toJsonString = toJsonString.Trim(',');
            toJsonString += "}";
            
            File.WriteAllText(path, toJsonString);

            Debug.Log(toJsonString);

            //List<float> wordScores = PlayerPrefsX.GetFloatArray(word).ToList();

            //if(wordScores.Count > 10)
            //{
            //    wordScores = wordScores.Skip(1).ToList();
            //}

            //wordScores.Add(wordScore);

            //PlayerPrefsX.SetFloatArray(word, wordScores.ToArray());
            //Debug.Log("Current Word: "+word +" Word score: " + wordScore);

            //foreach(float score in PlayerPrefsX.GetFloatArray(word))
            //{
            //    Debug.Log(score);
            //}
            shownWord.text += tempLetter.letterName;
            speakButton.SetActive(true);
            Destroy(currLetter);
        }
    }
}
