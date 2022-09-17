using UnityEngine;

namespace Cards
{
    public class CardDropArea : MonoBehaviour
    {
        public bool IsHovering {
            get {
                var myTransform = GetComponent<RectTransform>();
                
                var mousePosition = Input.mousePosition;
                
                Debug.Log($"Mouse position: {mousePosition} & Transform: {myTransform.rect}");
                return RectTransformUtility.RectangleContainsScreenPoint(myTransform, mousePosition);
            }
        }

    }
}