using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardSmallView : MonoBehaviour
    {
        [SerializeField] private Image cardIcon;

        public void Init(Card card)
        {
            cardIcon.sprite = card.icon;
        }
    }
}