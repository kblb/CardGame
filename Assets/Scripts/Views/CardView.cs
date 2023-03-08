﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(DragNotifier))]
public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject mask;
    [SerializeField] private Image highlightImage;

    public CardInstance cardInstance;

    [FormerlySerializedAs("draggableImage")] public DragNotifier dragNotifier;

    private TweenerCore<float, float, FloatOptions> currentTween;
    public event Action<CardView> OnBeginDragNotification;
    public event Action<CardView> OnDragNotification;
    public event Action<CardView> OnExitDragNotification;

    private void Awake()
    {
        dragNotifier = GetComponent<DragNotifier>();
        Highlight(false);

        DragNotifier di = GetComponent<DragNotifier>();
        di.OnBeginDragNotification += OnBeginDrag;
        di.OnDragNotification += OnDrag;
        di.OnExitDragNotification += OnExitDrag;
    }

    private void OnDrag()
    {
        OnDragNotification?.Invoke(this);
    }

    private void OnExitDrag()
    {
        OnExitDragNotification?.Invoke(this);
    }

    private void OnBeginDrag()
    {
        OnBeginDragNotification?.Invoke(this);
    }

    public void Init(CardInstance card)
    {
        this.cardInstance = card;
        cardIcon.sprite = card.scriptableObject.icon;
        titleText.text = card.scriptableObject.displayName;
        descriptionText.text = card.scriptableObject.description;
        
    }

    public void Highlight(bool highlight)
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        
        if (highlight)
        {
            Color color = highlightImage.color;
            color.a = 0;
            highlightImage.color = color;
            currentTween = DOTween.To(() => highlightImage.color.a, x => highlightImage.color = new Color(color.r, color.g, color.b, x), 1, 1);
        }

        mask.SetActive(highlight);
    }
}