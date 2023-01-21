using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;

    public event Action OnExitDragNotification;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnExitDragNotification?.Invoke();
    }
}