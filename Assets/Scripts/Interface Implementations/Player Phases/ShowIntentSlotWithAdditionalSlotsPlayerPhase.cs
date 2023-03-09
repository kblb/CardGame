using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowIntentSlotWithAdditionalSlotsPlayerPhase : IPlayerPhase
{
    private readonly FightView fightView;
    private readonly DeckInstance playersDeck;
    private CardView selectedCard;
    public event Action OnCompleted;

    private List<CardInstance> cardsHookedUpTo;

    public ShowIntentSlotWithAdditionalSlotsPlayerPhase(FightView fightView, DeckInstance playersDeck)
    {
        this.fightView = fightView;
        this.playersDeck = playersDeck;

        fightView.uiView.intentView.OnCommitClicked += InvokeOnCompleted;
    }

    public void Start()
    {
        fightView.ShowIntentOverTarget(playersDeck.intent);

        cardsHookedUpTo = new List<CardInstance>(playersDeck.hand.OfType<ModifyCardInstance>());

        fightView.uiView.Highlight(cardsHookedUpTo);

        foreach (CardInstance cardInstance in cardsHookedUpTo)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);

            cardView.OnBeginDragNotification += CardViewOnBeginDragNotification;
            cardView.OnDragNotification += CardViewOnOnDragNotification;
            cardView.OnExitDragNotification += CardViewOnExitDragNotification;
        }
    }

    private void InvokeOnCompleted()
    {
        foreach (CardInstance cardInstance in cardsHookedUpTo)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            cardView.OnBeginDragNotification -= CardViewOnBeginDragNotification;
            cardView.OnDragNotification -= CardViewOnOnDragNotification;
            cardView.OnExitDragNotification -= CardViewOnExitDragNotification;
        }


        fightView.uiView.intentView.Hide();
        OnCompleted?.Invoke();
    }

    private void CardViewOnBeginDragNotification(CardView cardView)
    {
        selectedCard = cardView;
        fightView.uiView.TurnOffCardHighlights();
        fightView.uiView.intentView.modifiersArea.transform.Highlight();
    }

    private void CardViewOnExitDragNotification(CardView cardView)
    {
        if (fightView.uiView.intentView.modifiersArea.IsHovering)
        {
            playersDeck.AddCardToIntent(cardView.cardInstance as ModifyCardInstance);
        }
        else
        {
            fightView.uiView.ShowHand(playersDeck.hand);
        }

        fightView.uiView.intentView.modifiersArea.transform.TurnOffHighlight();
        fightView.uiView.Highlight(playersDeck.hand.Where(t => t is ModifyCardInstance).ToList());
    }

    private void CardViewOnOnDragNotification(CardView cardView)
    {
        cardView.transform.position = Input.mousePosition;
    }
}