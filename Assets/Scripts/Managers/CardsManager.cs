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

        [Button]
        public void AddCard(Card card)
        {
            var cardObject = Instantiate(cardPrefab);
            cardObject.Init(card, HandleEndDrag);
            handView.AddCard(cardObject);
        }
        
        public void AddOnCommitListener(Action<List<Card>> listener)
        {
            cardQueue.AddOnCommitListener(listener);
        }

        private bool HandleEndDrag(Card card)
        {
            return cardDropArea.IsHovering && cardQueue.AddCard(card);
        }
    }
}