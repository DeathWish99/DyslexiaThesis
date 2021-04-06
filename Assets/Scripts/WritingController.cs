using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WritingController : LetterTraceClass
{
    public LetterTrace currLetter;
    public int currIndex;
    public Vector2[] edgeColliderPoints;
    [SerializeField]private Canvas canvas;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //ShootRayToImage();
    }

    public void ShootRayToImage()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        
        foreach (Vector2 point in edgeColliderPoints)
        {
            if (Physics.Raycast(new Vector3(point.x, point.y, transform.position.z), fwd, out hit))
                Debug.Log(hit.collider.gameObject.name);
            else
                Debug.Log("There is nothing in front of the object!");
        }
    }
}
