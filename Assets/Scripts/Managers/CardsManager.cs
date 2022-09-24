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

        [SerializeField, SceneObjectsOnly]
        private DeckController deckController;
        
        private void Start()
        {
            deckModel.Init(deck);
            deckModel.NewCardDrawn += OnCardDraw;

            deckController.Init();

            AdvanceTurn(null);
            cardQueue.AddOnCommitListener(AdvanceTurn);
        }
        
        private void AdvanceTurn(List<CardModelWrapper> cardsUsed)
        {
            deckModel.AdvanceTurn(cardsUsed);
        }

        private void OnCardDraw(CardModelWrapper card)
        {
            handView.AddCard(card, HandleEndDrag);
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