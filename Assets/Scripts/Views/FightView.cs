﻿using System;
using DG.Tweening;
using UnityEngine;

public class FightView : MonoBehaviour
{
    [SerializeField] public SlotsView slotsView;
    [SerializeField] public UIView uiView;

    public event Action OnCastFinished;

    public void OnCast(CardInstance card, ActorInstance target)
    {
        CardView cardView = uiView.FindCardView(card);
        ActorView actorView = slotsView.FindActorView(target);
        Vector3 actorViewScreenPosition = Camera.main.WorldToViewportPoint(actorView.transform.position);
        Vector3 actorUiPosition = new Vector3(Screen.width * actorViewScreenPosition.x, Screen.height * actorViewScreenPosition.y, 0);

        Vector3 originalCardPosition = cardView.transform.position;

        DOTween.Sequence()
            .Append(cardView.transform
                .DOMove(actorUiPosition, 0.5f)
                .SetEase(Ease.InCubic))
            .Append(actorView.transform
                .DOMove(actorView.transform.position + new Vector3(1, -1, 0) * 5, 0.2f)
                .SetEase(Ease.OutCirc)
                .SetLoops(2, LoopType.Yoyo))
            .Insert(0.5f, cardView.transform
                .DOMove(originalCardPosition, 0.5f)
                .SetEase(Ease.OutCubic))
            .AppendCallback(() => OnCastFinished?.Invoke())
            ;
    }
}