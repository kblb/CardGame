using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardQueueView))]
    public class CardQueue : MonoBehaviour
    {
        private const int MaxCards = 2;
        [SerializeField] [SceneObjectsOnly] private Button cardCommitButton;
        private CardQueueView _cardQueueView;
        private List<CardModelWrapper> _cards;

        private event Action<List<CardModelWrapper>> OnCommit;

        private void Awake()
        {
            _cards = new List<CardModelWrapper>();
            _cardQueueView = GetComponent<CardQueueView>();
            cardCommitButton.onClick.AddListener(Commit);
        }

        public bool AddCard(CardModelWrapper card)
        {
            if (_cards.Count >= MaxCards) return false;
            _cards.Add(card);
            _cardQueueView.AddCard(card.Model);
            return true;
        }

        public void AddOnCommitListener(Action<List<CardModelWrapper>> action)
        {
            OnCommit += action;
        }

        private void Clear()
        {
            _cards.Clear();
            _cardQueueView.Clear();
        }

        private void Commit()
        {
            OnCommit?.Invoke(_cards);
            Clear();
        }
    }
}