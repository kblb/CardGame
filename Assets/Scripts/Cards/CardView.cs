using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(DraggableImage))]
    public class CardView : MonoBehaviour
    {

        public Image cardIcon;
        private DraggableImage _draggableImage;

        private void Awake()
        {
            _draggableImage = GetComponent<DraggableImage>();
        }

        public void Init(Card card)
        {
            cardIcon.sprite = card.icon;
        }

        public void SetOnExitDragNotificationListener(Action redraw)
        {
            _draggableImage.OnExitDragNotification += redraw;
        }
    }
}