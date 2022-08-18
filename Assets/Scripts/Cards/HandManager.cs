using Sirenix.OdinInspector;
using UnityEngine;

namespace Cards
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private HandView handView;

        public void AddCard(Card card, CardView cardPrefab)
        {
            var cardObject = Instantiate(cardPrefab);
            cardObject.Init(card);
            handView.AddCard(cardObject);
        }
    }
}