using Sirenix.OdinInspector;
using UnityEngine;

namespace Cards
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private HandView handView;

        public void AddCard(CardView cardObject)
        {

            handView.AddCard(cardObject);
        }
    }
}