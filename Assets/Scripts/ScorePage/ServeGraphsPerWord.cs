using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class ServeGraphsPerWord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}


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
