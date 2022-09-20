using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    public class CardModelWrapper
    {
        public Card Model { get; }
        public int Id { get; }
        
        public CardModelWrapper(Card model, int id)
        {
            Model = model;
            Id = id;
        }
    }

    public class DeckModel : MonoBehaviour
    {
        private int _nextId;
        private List<CardModelWrapper> _drawPile;
        private List<CardModelWrapper> _playerHand;
        private List<CardModelWrapper> _discardPile;

        public event Action<CardModelWrapper> NewCardDrawn;
        public event Action<CardModelWrapper> NewCardDiscarded;
        public event Action<List<CardModelWrapper>> DrawPileReshuffled;

        public void Init(IEnumerable<Card> deck)
        {
            _drawPile = deck.Select((c, i) => new CardModelWrapper(c, i)).ToList();
            _playerHand = new List<CardModelWrapper>();
            _discardPile = new List<CardModelWrapper>();
            _nextId = _drawPile.Count;
            DrawPileReshuffled?.Invoke(_drawPile);
        }

        public void DrawCard()
        {
            if (_drawPile.Count == 0)
            {
                ReshuffleDeck();
            }
            var card = _drawPile[0];
            _drawPile.RemoveAt(0);
            _playerHand.Add(card);
            NewCardDrawn?.Invoke(card);
        }

        public void ReshuffleDeck()
        {
            _drawPile = _discardPile;
            _discardPile = new List<CardModelWrapper>();
            DrawPileReshuffled?.Invoke(_drawPile);
        }

        public void DiscardCard(int id)
        {
            var card = _playerHand.First(c => c.Id == id);
            _playerHand.Remove(card);
            _discardPile.Add(card);
            NewCardDiscarded?.Invoke(card);
        }

        public void AdvanceTurn(List<CardModelWrapper> cardsUsed)
        {
            if (cardsUsed != null)
            {
                foreach (var card in cardsUsed)
                {
                    DiscardCard(card.Id);
                }
            }
            
            while (_playerHand.Count < 5)
            {
                DrawCard();
            }
        }
    }
}