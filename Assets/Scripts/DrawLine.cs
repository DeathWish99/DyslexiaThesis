using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : LetterTraceClass
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public Camera uiCam;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> fingerPositions;

    public LetterTrace currLetter;
    public int currIndex;

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
        linePrefab.GetComponent<WritingController>().currLetter = currLetter;
        linePrefab.GetComponent<WritingController>().currIndex = currIndex;
        linePrefab.GetComponent<WritingController>().edgeColliderPoints = edgeCollider.points;
        linePrefab.GetComponent<WritingController>().ShootRayToImage();
    }
}
