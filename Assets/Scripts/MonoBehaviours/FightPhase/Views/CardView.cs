using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DraggableImage))]
public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    private DraggableImage _draggableImage;

    private void Awake()
    {
        _draggableImage = GetComponent<DraggableImage>();
    }

    public void Init(CardInstance card)
    {
        cardIcon.sprite = card.scriptableObject.icon;
        titleText.text = card.scriptableObject.displayName;
        descriptionText.text = card.scriptableObject.description;
    }

    public void SetOnExitDragNotificationListener(Action action)
    {
        _draggableImage.OnExitDragNotification += action;
    }
}