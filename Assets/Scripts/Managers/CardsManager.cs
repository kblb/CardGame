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
        
        [SerializeField, SceneObjectsOnly]
        private DeckModel deckModel;

        private void Start()
        {
            deckModel.Init(deck);
            deckModel.NewCardDrawn += OnCardDraw;

            AdvanceRound(null);
            cardQueue.AddOnCommitListener(AdvanceRound);
        }
        
        private void AdvanceRound(List<CardModelWrapper> cardsUsed)
        {
            deckModel.AdvanceTurn(cardsUsed);
        }

        public void OnCardDraw(CardModelWrapper card)
        {
            var cardObject = Instantiate(cardPrefab);
            cardObject.Init(card, HandleEndDrag);
            handView.AddCard(cardObject);
        }

        public void AddOnCommitListener(Action<List<CardModelWrapper>> listener)
        {
            cardQueue.AddOnCommitListener(listener);
        }

        private bool HandleEndDrag(CardModelWrapper card)
        {
            return cardDropArea.IsHovering && cardQueue.AddCard(card);
        }
    }
}