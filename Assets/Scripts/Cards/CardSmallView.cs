using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(Image))]
    public class CardSmallView : MonoBehaviour
    {
        [SerializeField]
        private Image cardIcon;

        private void Awake()
        {
            cardIcon = GetComponent<Image>();
        }

        public void Init(Card card)
        {
            cardIcon.sprite = card.icon;
        }
    }
}