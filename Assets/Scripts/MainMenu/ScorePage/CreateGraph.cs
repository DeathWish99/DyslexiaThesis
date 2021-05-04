using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CreateGraph
{

    public static List<GameObject> ShowGraph(List<float> valueList, RectTransform graphContainer, Sprite circleSprite, RectTransform labelTemplateX, RectTransform labelTemplateY)
    {
        List<GameObject> graphObjects = new List<GameObject>();
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float padding = 20f;
        float xSize = graphContainer.sizeDelta.x / valueList.Count;

        GameObject lastCircleObj = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = padding + i * xSize;
            float yPosition = (Mathf.Round(valueList[i]) / yMaximum) * graphHeight;
            GameObject circleObj = CreateCircle(new Vector2(xPosition, yPosition), graphContainer, circleSprite);
            graphObjects.Add(circleObj);
            if(lastCircleObj != null)
            {
                graphObjects.Add(CreateDotConnection(lastCircleObj.GetComponent<RectTransform>().anchoredPosition, circleObj.GetComponent<RectTransform>().anchoredPosition, graphContainer));
            }
            lastCircleObj = circleObj;
        }

        return graphObjects;
    }

    static GameObject CreateCircle(Vector2 anchoredPosition, RectTransform graphContainer, Sprite circleSprite)
    {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(graphContainer, false);
        circle.GetComponent<Image>().sprite = circleSprite;
        RectTransform rect = circle.GetComponent<RectTransform>();
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = new Vector2(11, 11);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);

        return circle;
    }

    static GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, RectTransform graphContainer)
    {
        GameObject dotConnection = new GameObject("dotConnection", typeof(Image));
        dotConnection.transform.SetParent(graphContainer, false);
        dotConnection.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rect = dotConnection.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rect.sizeDelta = new Vector2(distance, 3f);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.anchoredPosition = dotPositionA + dir * distance * .5f;
        rect.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        return dotConnection;
    }
}
