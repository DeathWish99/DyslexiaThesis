using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

public class ServeGraphsPerWord : MonoBehaviour
{
    private string path;
    private string fromJsonString;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/ScoreRecords.json";
        fromJsonString = File.ReadAllText(path);
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
