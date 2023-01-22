using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

public class CardCommitAreaView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public Button cardCommitButton;
    [SerializeField] public IsMouseHoveringOverMe isMouseHoveringOverMe;

    public event Action OnCommitClicked;

    private void Awake()
    {
        cardCommitButton.interactable = false;
    }

    public void Highlight(bool isHovering)
    {
        transform.localScale = Vector3.one * (isHovering ? 1.2f : 1.0f);
    }
}