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
        private List<Card> _cards;

        private void Awake()
        {
            _cards = new List<Card>();
            _cardQueueView = GetComponent<CardQueueView>();
            cardCommitButton.onClick.AddListener(Commit);
        }
        private event Action<List<Card>> OnCommit;

        public bool AddCard(Card card)
        {
            if (_cards.Count >= MaxCards) return false;
            _cards.Add(card);
            _cardQueueView.AddCard(card);
            return true;
        }

        public void AddOnCommitListener(Action<List<Card>> action)
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