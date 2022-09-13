using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class CardQueueView : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] private CardSmallView cardPrefab;
        private List<CardSmallView> _cardViews;
        private Transform _transform;


        private void Awake()
        {
            _cardViews = new List<CardSmallView>();
            _transform = transform;
        }

        public void AddCard(Card card)
        {
            var cardView = Instantiate(cardPrefab, _transform);
            cardView.Init(card);
            _cardViews.Add(cardView);
        }

        public void Clear()
        {
            for (var i = 0; i < _cardViews.Count; i++)
            {
                var cardView = _cardViews[i];
                Destroy(cardView.gameObject);
            }
            _cardViews = new List<CardSmallView>();
        }
    }
}