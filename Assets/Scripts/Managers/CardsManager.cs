using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] private CardView cardPrefab;
        [SerializeField] [SceneObjectsOnly] private HandView handView;
        [SerializeField] [SceneObjectsOnly] private CardQueue cardQueue;
        [SerializeField] [SceneObjectsOnly] private CardDropArea cardDropArea;

        [SerializeField] private List<Card> deck;

        [SerializeField] [SceneObjectsOnly] private DeckModel deckModel;

        [SerializeField] [SceneObjectsOnly] private DeckController deckController;

        private AnimationQueue _animationQueue;
        private const int HandCardCount = 5;

        public void Init(AnimationQueue animationQueue)
        {
            _animationQueue = animationQueue;
            deckModel.Init(deck);
            deckModel.NewCardDrawn += OnCardDraw;
            deckModel.NewCardDrawn += OnCardDrawLog;
            deckModel.NewCardDiscarded += OnNewCardDiscardedLog;
            deckModel.DrawPileReshuffled += OnDrawPileReshuffledLog;

            deckController.Init();

            AdvanceTurn(null);
            cardQueue.AddOnCommitListener(AdvanceTurn);
        }

        private void OnDrawPileReshuffledLog(List<CardModelWrapper> obj)
        {
            Debug.Log($"Draw pile reshuffled {obj.Select(t => t.Model.displayName).Aggregate((t, y) => t + ", " + y)}");
        }

        private void OnNewCardDiscardedLog(CardModelWrapper obj)
        {
            Debug.Log($"Card discarded {obj.Model.displayName}");
        }

        private void OnCardDrawLog(CardModelWrapper obj)
        {
            Debug.Log($"Card draw {obj.Model.displayName}");
        }

        private void AdvanceTurn(List<CardModelWrapper> cardsUsed)
        {
            if (cardsUsed != null)
            {
                foreach (var card in cardsUsed)
                {
                    deckModel.DiscardCard(card.Id);
                }
            }

            int numberToPull = HandCardCount - deckModel.playerHand.Count;
            for (int i = 0; i < numberToPull; i++)
            {
                _animationQueue.AddElement(() =>
                {
                    deckModel.DrawCard();
                });
            }
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