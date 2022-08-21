using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardQueue : MonoBehaviour
    {
        private const int MaxCards = 2;
        private List<Card> _cards;

        private void Awake()
        {
            _cards = new List<Card>();
        }

        public bool AddCard(Card card)
        {
            if (_cards.Count >= MaxCards) return false;
            _cards.Add(card);
            return true;
        }
    }
}