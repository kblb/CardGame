using UnityEngine;

public class IsMouseHoveringOverMe : MonoBehaviour
{
    public bool IsHovering =>
        RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition);
}