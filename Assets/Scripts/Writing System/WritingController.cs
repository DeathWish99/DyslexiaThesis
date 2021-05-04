using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WritingController : LetterTraceClass
{
    public Vector2[] edgeColliderPoints;

    [SerializeField]private Canvas canvas;
    private List<GameObject> objectsDetectedbyRay = new List<GameObject>();
    

    public List<GameObject> ShootRayToImage(GameObject currentLine)
    {
        int overflowCount = 0;
        objectsDetectedbyRay.Clear();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        
        foreach (Vector2 point in edgeColliderPoints)
        {
            if (Physics.Raycast(new Vector3(point.x, point.y, transform.position.z), fwd, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                objectsDetectedbyRay.Add(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("There is nothing in front of the object!");
                overflowCount++;
            }
        }

        if(overflowCount < 100 && objectsDetectedbyRay.Count > 0)
        {
            return objectsDetectedbyRay;
        }
        else
        {
            Destroy(currentLine);
            return null;
        }
    }
}
