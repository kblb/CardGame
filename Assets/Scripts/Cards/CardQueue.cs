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
        private List<(int, Card)> _cards;

        private event Action<List<(int, Card)>> OnCommit;

        private void Awake()
        {
            _cards = new List<(int, Card)>();
            _cardQueueView = GetComponent<CardQueueView>();
            cardCommitButton.onClick.AddListener(Commit);
        }

        public bool AddCard(Card card, int cardId)
        {
            if (_cards.Count >= MaxCards) return false;
            _cards.Add((cardId, card));
            _cardQueueView.AddCard(card);
            return true;
        }

        public void AddOnCommitListener(Action<List<(int, Card)>> action)
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