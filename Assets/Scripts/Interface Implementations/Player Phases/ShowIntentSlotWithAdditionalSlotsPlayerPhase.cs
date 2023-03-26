using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowIntentSlotWithAdditionalSlotsPlayerPhase : IPlayerPhase
{
    private readonly FightView fightView;
    private readonly DeckInstance playersDeck;
    public event Action OnCompleted;
    public event Action OnCancel;

    private List<CardInstance> cardsHookedUpTo;
    private CardInstance attackCard;

    public ShowIntentSlotWithAdditionalSlotsPlayerPhase(FightView fightView, DeckInstance playersDeck)
    {
        this.fightView = fightView;
        this.playersDeck = playersDeck;

        fightView.uiView.intentView.OnCommitClicked += () =>
        {
            UnhookAll();

            fightView.uiView.intentView.Hide();
            OnCompleted?.Invoke();
        };
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

        attackCard = playersDeck.intent.attack;

        CardView attackCardView = fightView.uiView.FindCardView(attackCard);
        attackCardView.OnBeginDragNotification += AttackCardViewOnBeginDragNotification;
        attackCardView.OnDragNotification += AttackCardViewOnDragNotification;
        attackCardView.OnExitDragNotification += AttackCardViewOnExitDragNotification;
    }

    public void Terminate()
    {
        UnhookAll();
    }

    private void UnhookAll()
    {
        if (cardsHookedUpTo != null)
        {
            foreach (CardInstance cardInstance in cardsHookedUpTo)
            {
                CardView cardView = fightView.uiView.FindCardView(cardInstance);
                cardView.OnBeginDragNotification -= CardViewOnBeginDragNotification;
                cardView.OnDragNotification -= CardViewOnOnDragNotification;
                cardView.OnExitDragNotification -= CardViewOnExitDragNotification;
            }
        }

        if (attackCard != null)
        {
            CardView attackCardView = fightView.uiView.FindCardView(attackCard);
            attackCardView.OnBeginDragNotification -= AttackCardViewOnBeginDragNotification;
            attackCardView.OnDragNotification -= AttackCardViewOnDragNotification;
            attackCardView.OnExitDragNotification -= AttackCardViewOnExitDragNotification;
        }
    }

    private void AttackCardViewOnBeginDragNotification(CardView obj)
    {
        fightView.uiView.TurnOffCardHighlights();
    }

    private void AttackCardViewOnExitDragNotification(CardView obj)
    {
        if (fightView.uiView.intentView.modifiersArea.IsHovering)
        {
            fightView.uiView.ShowIntent(playersDeck.intent);
        }
        else
        {
            UnhookAll();
            fightView.uiView.TurnOffCardHighlights();
            playersDeck.CancelIntentBackToHand();
            fightView.uiView.intentView.Hide();
            OnCancel?.Invoke();
        }
    }

    private void AttackCardViewOnDragNotification(CardView obj)
    {
        obj.transform.position = Input.mousePosition;
    }

    private void CardViewOnBeginDragNotification(CardView cardView)
    {
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

            playersDeck.intent.modifiers.Remove(cardView.cardInstance as ModifyCardInstance);
            playersDeck.hand.Add(cardView.cardInstance);
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