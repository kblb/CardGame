using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(DraggableImage))]
    public class CardView : MonoBehaviour, IDragEventHandler
    {
        [SerializeField] private Image cardIcon;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private DamageIndicatorsView damageIndicators;
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

            Destroy(this.gameObject);
            return true;
        }

        public void Init(CardModelWrapper card, Func<CardModelWrapper, bool> onDragEnd)
        {
            _card = card;
            cardIcon.sprite = card.Model.icon;
            titleText.text = card.Model.displayName;
            descriptionText.text = card.Model.description;
            _onDragEnd = onDragEnd;
            _draggableImage.Init(this);
            damageIndicators.SetEffects(card.Model);
        }

        public void SetOnExitDragNotificationListener(Action action)
        {
            _draggableImage.OnExitDragNotification += action;
        }
    }
}