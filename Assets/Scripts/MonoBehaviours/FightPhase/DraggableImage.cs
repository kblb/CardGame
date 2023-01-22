using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action OnExitDragNotification;
    public event Action OnDragNotification;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        OnDragNotification?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnExitDragNotification?.Invoke();
    }
}