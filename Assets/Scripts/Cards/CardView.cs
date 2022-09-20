﻿using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(DraggableImage))]
    public class CardView : MonoBehaviour, IDragEventHandler
    {
        [SerializeField] private Image cardIcon;
        private CardModelWrapper _card;
        private DraggableImage _draggableImage;
        private Func<CardModelWrapper, bool> _onDragEnd;

        private void Awake()
        {
            _draggableImage = GetComponent<DraggableImage>();
        }

        public bool HandleEndDrag(Vector3 startPosition, Vector3 endPosition)
        {
            if (_onDragEnd == null || !_onDragEnd(_card)) return false;

            Destroy(gameObject);
            return true;
        }

        public void Init(CardModelWrapper card, Func<CardModelWrapper, bool> onDragEnd)
        {
            _card = card;
            cardIcon.sprite = card.Model.icon;
            _onDragEnd = onDragEnd;
            _draggableImage.Init(this);
        }

        public void SetOnExitDragNotificationListener(Action redraw)
        {
            _draggableImage.OnExitDragNotification += redraw;
        }
    }
}