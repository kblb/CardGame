﻿using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public DiscardPileView discardPileView;
    [SerializeField] [SceneObjectsOnly] public DrawPileView drawPileView;
    [SerializeField] public HandView handView;
    [SerializeField] public Button endTurn;

    [SerializeField] [AssetsOnly] public CardView cardViewPrefab;

    public readonly List<CardView> cardViews = new();

    public CardView CreateCardView(CardInstance cardInstance)
    {
        CardView cardView = Instantiate(cardViewPrefab, handView.transform);
        cardView.Init(cardInstance);
        cardViews.Add(cardView);
        cardView.name += cardViews.Count;
        return cardView;
    }

    public void ShowDrawPile(List<CardInstance> drawPile)
    {
        ShowCardsIn(drawPile, cardViews, drawPileView.transform.position, 3, 2);
    }

    public void ShowHand(List<CardInstance> cardInstances)
    {
        ShowCardsIn(cardInstances, cardViews, handView.transform.position, 60, 7);
    }

    public void ShowDiscardPile(List<CardInstance> discardPile)
    {
        ShowCardsIn(discardPile, cardViews, discardPileView.transform.position, 3, 2);
    }

    private static void ShowCardsIn(List<CardInstance> instances, List<CardView> views, Vector3 position, float spacing, float angle)
    {
        IOrderedEnumerable<CardView> viewsOrdered = views
            .Where(t => instances.Any(y => y == t.cardInstance))
            .OrderBy(t => instances.IndexOf(t.cardInstance));

        int i = 0;
        int middleIndex = viewsOrdered.Count() / 2;

        float offset = (viewsOrdered.Count() - 1) * spacing / 2;
        float angleOffset = (viewsOrdered.Count() - 1) * angle / 2;
        foreach (CardView cardView in viewsOrdered)
        {
            float currentAngle = -(i * angle - angleOffset);
            float currentX = i * spacing - offset;
            float currentY = 0.2f * (i * spacing - offset);
            currentY *= i >= middleIndex ? -1 : 1;

            int siblingIndex = cardView.transform.parent.childCount - 1;
            cardView.NewPosition(position + new Vector3(currentX, currentY, 0), new Vector3(0, 0, currentAngle), siblingIndex, .5f);
            i++;
        }
    }

    public CardView FindCardView(CardInstance card)
    {
        return cardViews.Find(t => t.cardInstance == card);
    }

    public void Highlight(List<CardInstance> instances)
    {
        TurnOffCardHighlights();

        IOrderedEnumerable<CardView> viewsOrdered = cardViews
            .Where(t => instances.Any(y => y == t.cardInstance))
            .OrderBy(t => instances.IndexOf(t.cardInstance));

        foreach (CardView cardView in viewsOrdered)
        {
            cardView.Highlight(true);
        }
    }

    public void TurnOffCardHighlights()
    {
        foreach (CardView cardView in cardViews)
        {
            cardView.Highlight(false);
        }
    }

    public void MoveUpCard(CardView cardView)
    {
        cardView.MoveUp();
    }

    public void MoveBackDownCard(CardView cardView)
    {
        cardView.RestoreToOriginalPosition();
    }
}