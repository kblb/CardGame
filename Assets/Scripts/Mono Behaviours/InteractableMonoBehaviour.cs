using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Notifies using events when it's dragged and pointed
 */
[RequireComponent(typeof(DragNotifier))]
public class InteractableMonoBehaviour<T> : MonoBehaviour where T : class
{
    public event Action<T> OnBeginDragNotification;
    public event Action<T> OnDragNotification;
    public event Action<T> OnExitDragNotification;
    public event Action<T> OnPointerEnterNotification;
    public event Action<T> OnPointerExitNotification;
    
    [SerializeField] private DragNotifier dragNotifier;

    protected void Awake()
    {
        dragNotifier.OnBeginDragNotification += OnBeginDrag;
        dragNotifier.OnDragNotification += OnDrag;
        dragNotifier.OnExitDragNotification += OnExitDrag;
        dragNotifier.OnPointerEnterNotification += OnPointerEnter;
        dragNotifier.OnPointerExitNotification += OnPointerExit;
    }
    
    private void OnPointerExit()
    {
        OnPointerExitNotification?.Invoke( this as T);
    }

    private void OnPointerEnter()
    {
        OnPointerEnterNotification?.Invoke(this as T);
    }

    private void OnDrag()
    {
        OnDragNotification?.Invoke(this as T);
    }

    private void OnExitDrag()
    {
        OnExitDragNotification?.Invoke(this as T);
    }

    private void OnBeginDrag()
    {
        OnBeginDragNotification?.Invoke(this as T);
    }
    
    public static T2 IsPointerOverType<T2>() where T2 : MonoBehaviour
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        RaycastResult rr = results.FirstOrDefault(t => t.gameObject.GetComponentInParent<T2>());
        return rr.gameObject?.GetComponentInParent<T2>();
    }
}
