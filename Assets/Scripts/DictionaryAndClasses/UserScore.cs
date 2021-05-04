using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
