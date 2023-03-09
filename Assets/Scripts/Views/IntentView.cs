using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class IntentView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public Button cardCommitButton;
    [SerializeField] public IsMouseHoveringOverMe attackArea, modifiersArea;

    public event Action OnShown;
    public event Action OnCommitClicked;

    private void Awake()
    {
        cardCommitButton.onClick.AddListener(() => OnCommitClicked?.Invoke());
        this.transform.localScale = Vector3.zero;
    }

    public void ShowOverTarget(IntentInstance playersDeckIntent, SlotView slotView)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(slotView.transform.position);
        screenPos.y = 60 / 100f * Screen.height; //move up 
        screenPos.x -= 35 / 100f * Screen.width; //move to the left

        this.transform.position = screenPos;
        this.transform
            .DOScale(Vector3.one, 1.5f)
            .OnUpdate(() => { OnShown?.Invoke(); });
    }

    public void Hide()
    {
        this.transform
            .DOScale(Vector3.zero, 0.5f);
    }
}