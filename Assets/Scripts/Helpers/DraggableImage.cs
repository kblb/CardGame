using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers
{
    public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IDragEventHandler _dragEventHandler;
        private Vector3 _startPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragEventHandler != null && _dragEventHandler.HandleEndDrag(_startPosition, transform.position)) return;

            transform.position = _startPosition;
            OnExitDragNotification?.Invoke();
        }

        public event Action OnExitDragNotification;

        public void Init(IDragEventHandler dragEventHandler)
        {
            _dragEventHandler = dragEventHandler;
        }
    }

    public interface IDragEventHandler
    {
        /// <summary>
        /// Returns true if the drag event was consumed by the handler.
        /// The card object should also immediately be destroyed by the parent handler
        /// </summary>
        bool HandleEndDrag(Vector3 startPosition, Vector3 endPosition);
    }
}