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
        private List<Card> _cards;
        private CardQueueView _cardQueueView;
        [SerializeField] [SceneObjectsOnly]
        private Button cardCommitButton;

        private void Awake()
        {
            _cards = new List<Card>();
            _cardQueueView = GetComponent<CardQueueView>();
            cardCommitButton.onClick.AddListener(Commit);
        }

        public bool AddCard(Card card)
        {
            if (_cards.Count >= MaxCards) return false;
            _cards.Add(card);
            _cardQueueView.AddCard(card);
            return true;
        }

        private void Clear()
        {
            _cards.Clear();
            _cardQueueView.Clear();
        }

        private void Commit()
        {
            Clear();
            Debug.Log("Commited");
        }
    }
}