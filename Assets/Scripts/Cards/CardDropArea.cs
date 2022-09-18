using UnityEngine;

namespace Cards
{
    public class CardDropArea : MonoBehaviour
    {
        public bool IsHovering => RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition);
    }
}