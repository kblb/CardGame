using Unity.VisualScripting;
using UnityEngine;

public class HandView : MonoBehaviour
{
    public IsMouseHoveringOverMe isMouseHoveringOverMe;
    public int spacing = 30;

    private void Awake()
    {
        isMouseHoveringOverMe = transform.AddComponent<IsMouseHoveringOverMe>();
    }
}