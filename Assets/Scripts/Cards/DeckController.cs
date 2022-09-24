using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cards
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private HandView handView;
        [SerializeField] [SceneObjectsOnly] private DrawPileView drawPileView;
        [SerializeField] [SceneObjectsOnly] private DiscardPileView discardPileView;
        [SerializeField] [SceneObjectsOnly] private DeckModel deckModel;

        public void Init()
        {
            deckModel.NewCardDrawn += OnDrawNewCard;
            deckModel.NewCardDiscarded += OnDiscardCard;
            deckModel.DrawPileReshuffled += OnDrawPileReshuffled;

            discardPileView.Zero();
            drawPileView.Recount(deckModel.DrawPile.Count());
        }
        private void OnDrawPileReshuffled(List<CardModelWrapper> cards)
        {
            discardPileView.Zero();
            drawPileView.Recount(cards.Count);
        }

        private void OnDiscardCard(CardModelWrapper card)
        {
            discardPileView.AddCard();
        }

        private void OnDrawNewCard(CardModelWrapper card)
        {
            drawPileView.RemoveCard();
        }
    }
}