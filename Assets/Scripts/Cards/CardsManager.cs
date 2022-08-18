using Sirenix.OdinInspector;
using UnityEngine;

// TODO:
// - React to card being dropped on drop area
// - Add dropped cards to `two card pool`
// - Add "commit attack" button
// - Add enemy list / basic scriptable enemies
// - Add player stats/hp & enemy attack

namespace Cards
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] private CardView cardPrefab;

        [SerializeField] [SceneObjectsOnly] private HandManager handManager;

        [Button]
        public void AddCard(Card card)
        {
            handManager.AddCard(card, cardPrefab);
        }
    }
}