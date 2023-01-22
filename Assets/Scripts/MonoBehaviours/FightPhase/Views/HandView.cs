using Unity.VisualScripting;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] public float spacing = 30f;
    public IsMouseHoveringOverMe isMouseHoveringOverMe;

    private void Awake()
    {
        isMouseHoveringOverMe = transform.AddComponent<IsMouseHoveringOverMe>();
    }

    public void Highlight(bool isHovering)
    {
        transform.localScale = Vector3.one * (isHovering ? 1.2f : 1.0f);
    }
}