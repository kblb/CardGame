using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNotifier : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnExitDragNotification;
    public event Action OnDragNotification;
    public event Action OnBeginDragNotification;
    public event Action OnPointerEnterNotification;
    public event Action OnPointerExitNotification;
    
    private bool isDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        OnBeginDragNotification?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragNotification?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        OnExitDragNotification?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //sometimes we're getting false positives on pointer exit. If this happens while we're dragging, then ignore.
        if (isDragging == false)
        {
            OnPointerEnterNotification?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //sometimes we're getting false positives on pointer exit. If this happens while we're dragging, then ignore.
        if (isDragging == false) 
        {
            OnPointerExitNotification?.Invoke();
        }
    }
}