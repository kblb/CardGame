using System;
using System.Collections.Generic;
using Cards;
using Sirenix.OdinInspector;
using UnityEngine;

// TODO:
// - Add player stats/hp & enemy attack

namespace Managers
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] private CardView cardPrefab;
        [SerializeField] [SceneObjectsOnly] private HandView handView;
        [SerializeField] [SceneObjectsOnly] private CardQueue cardQueue;
        [SerializeField] [SceneObjectsOnly] private CardDropArea cardDropArea;

        [SerializeField]
        private List<Card> deck;
        private List<Card> _drawPile;
        private List<Card> _playerHand;
        private List<Card> _discardPile;

        private void Start()
        {
            _drawPile = new List<Card>(deck);
            _playerHand = new List<Card>();
            _discardPile = new List<Card>();
            
            AdvanceRound(null);
            cardQueue.AddOnCommitListener(AdvanceRound);
        }
        
        private void AdvanceRound(List<(int, Card)> cardsUsed)
        {
            if (cardsUsed != null)
            {
                cardsUsed.Sort((a, b) => b.Item1.CompareTo(a.Item1));
                foreach (var (index, card) in cardsUsed)
                {
                    _playerHand.RemoveAt(index);
                    _discardPile.Add(card);
                }
            }

            while (_playerHand.Count < 5)
            {
                if (_drawPile.Count == 0)
                {
                    _drawPile = new List<Card>(_discardPile);
                    _discardPile.Clear();
                }
                
                var card = _drawPile[0];
                _drawPile.RemoveAt(0);
                _playerHand.Add(card);
                DrawCard(card, _playerHand.Count - 1);
            }
        }

        private void DrawCard(Card card, int cardId)
        {
            var cardObject = Instantiate(cardPrefab);
            cardObject.Init(card, (c) => HandleEndDrag(c, cardId));
            handView.AddCard(cardObject);
        }

        public void AddOnCommitListener(Action<List<(int, Card)>> listener)
        {
            cardQueue.AddOnCommitListener(listener);
        }

        private bool HandleEndDrag(Card card, int cardId)
        {
            return cardDropArea.IsHovering && cardQueue.AddCard(card, cardId);
        }
    }
}