using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class CardCommitAreaView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public Button cardCommitButton;
    [SerializeField] public IsMouseHoveringOverMe isMouseHoveringOverMe;

    public event Action OnShown;
    public event Action OnCommitClicked;

    private void Awake()
    {
        cardCommitButton.onClick.AddListener(() => OnCommitClicked?.Invoke());
    }

    public void Highlight(bool isHovering)
    {
        transform.localScale = Vector3.one * (isHovering ? 1.2f : 1.0f);
    }

    public void ShowOverTarget(List<CardInstance> intent, SlotView slotView)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(slotView.transform.position);
        screenPos.y = Screen.height / 2; //move to the middle of the screen
        screenPos.x -= 2 / 10f * Screen.width; //move 20% to the left

        this.transform.DOScale(Vector3.one, 0.5f);
        this.transform
            .DOMove(screenPos, 1.0f)
            .OnUpdate(() => { OnShown?.Invoke(); });
    }

    public void Hide()
    {
        this.transform
            .DOScale(Vector3.zero, 0.5f);
    }
}