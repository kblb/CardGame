﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCardAndMoveUpPlayerPhase : IPlayerPhase
{
    private readonly FightView fightView;
    private readonly BattleInstance battleInstance;

    public ActorView selectedTarget;
    public CardView selectedCard;

    private List<CardInstance> cardsHookedUpTo;

    public event Action OnCancel;
    public event Action OnCompleted;

    public SelectCardAndMoveUpPlayerPhase(FightView fightView, BattleInstance battleInstance)
    {
        this.fightView = fightView;
        this.battleInstance = battleInstance;
    }

    public void Start()
    {
        cardsHookedUpTo = new List<CardInstance>(battleInstance.Player.deck.hand.OfType<AttackCardInstance>());

        //hooking events
        foreach (CardInstance cardInstance in cardsHookedUpTo)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);

            cardView.OnBeginDragNotification += CardViewDraggableImageOnBeginDragNotification;
            cardView.OnDragNotification += CardViewOnOnDragNotification;
            cardView.OnExitDragNotification += CardViewDraggableImageOnExitDragNotification;
        }

        foreach (ActorInstance enemy in battleInstance.allEnemies)
        {
            ActorView enemyView = fightView.slotsView.FindActorView(enemy);
            enemyView.OnMouseOverEvent += ActorViewOnOnMouseOverEvent;
            enemyView.OnMouseExitEvent += ActorViewOnOnMouseExitEvent;
        }

        HightlightDefaultState();
    }

    private void InvokeOnCompleted()
    {
        //unhook events
        foreach (CardInstance cardInstance in cardsHookedUpTo)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            cardView.OnBeginDragNotification -= CardViewDraggableImageOnBeginDragNotification;
            cardView.OnDragNotification -= CardViewOnOnDragNotification;
            cardView.OnExitDragNotification -= CardViewDraggableImageOnExitDragNotification;
        }

        foreach (ActorInstance enemy in battleInstance.allEnemies)
        {
            ActorView enemyView = fightView.slotsView.FindActorView(enemy);
            enemyView.OnMouseOverEvent -= ActorViewOnOnMouseOverEvent;
            enemyView.OnMouseExitEvent -= ActorViewOnOnMouseExitEvent;
        }

        selectedTarget.TurnOffSelect();

        //turn off all highlights
        fightView.slotsView.TurnOffHighlight(battleInstance.allEnemies);
        fightView.uiView.TurnOffCardHighlights();

        OnCompleted?.Invoke();
    }

    private void CardViewOnOnDragNotification(CardView obj)
    {
        obj.transform.position = Input.mousePosition;
    }

    private void HightlightDefaultState()
    {
        fightView.uiView.ShowHand(battleInstance.Player.deck.hand);
        fightView.uiView.Highlight(cardsHookedUpTo);
        foreach (ActorView actorView in fightView.slotsView.actorViews)
        {
            actorView.TurnOffHighlight();
        }
    }

    private void ActorViewOnOnMouseOverEvent(ActorView actorView)
    {
        if (selectedCard != null)
        {
            selectedTarget = actorView;
            actorView.Selected();
        }
    }

    private void ActorViewOnOnMouseExitEvent(ActorView actorView)
    {
        selectedTarget = null;
        actorView.TurnOffSelect();
    }

    private void CardViewDraggableImageOnBeginDragNotification(CardView cardView)
    {
        selectedCard = cardView;
        fightView.uiView.TurnOffCardHighlights();
        fightView.slotsView.Highlight(battleInstance.allEnemies);
    }

    private void CardViewDraggableImageOnExitDragNotification(CardView cardView)
    {
        if (selectedTarget != null)
        {
            InvokeOnCompleted();
        }
        else
        {
            HightlightDefaultState();
        }
    }
}