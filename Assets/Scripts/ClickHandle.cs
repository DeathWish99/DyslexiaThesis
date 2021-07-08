using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent upEvent;
    public UnityEvent downEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        downEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        upEvent?.Invoke();
    }
}
