using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action OnExitDragNotification;
    public event Action OnDragNotification;
    public event Action OnBeginDragNotification;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragNotification?.Invoke();
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