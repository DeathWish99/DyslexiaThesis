using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandle : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent upEvent;
    public UnityEvent downEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        downEvent?.Invoke();
    }
}
