using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class CardDropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public bool IsHovering { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHovering = false;
        }
    }
}