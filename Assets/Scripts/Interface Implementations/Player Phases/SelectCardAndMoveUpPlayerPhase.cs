using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        OnCompleted += UnhookEvents;
    }

    private void UnhookEvents()
    {
        //unhook events
        foreach (CardInstance cardInstance in cardsHookedUpTo)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            cardView.OnBeginDragNotification -= CardViewDraggableImageOnBeginDragNotification;
            cardView.OnDragNotification -= CardViewOnOnDragNotification;
            cardView.OnExitDragNotification -= CardViewDraggableImageOnExitDragNotification;
        }
        
        List<ActorInstance> actors = battleInstance.allEnemies;
        actors.Add(battleInstance.Player);

        foreach (ActorInstance enemy in actors)
        {
            ActorView enemyView = fightView.slotsView.FindActorView(enemy);
            enemyView.OnMouseOverEvent -= ActorViewOnOnMouseOverEvent;
            enemyView.OnMouseExitEvent -= ActorViewOnOnMouseExitEvent;
        }

        if (selectedTarget != null)
        {
            selectedTarget.TurnOffSelect();
        }

        //turn off all highlights
        fightView.slotsView.TurnOffHighlight(battleInstance.allEnemies);
        fightView.uiView.TurnOffCardHighlights();
    }

    public void Start()
    {
        cardsHookedUpTo = new List<CardInstance>(battleInstance.Player.inventory.deck.hand.OfType<AttackCardInstance>());

        //hooking events
        foreach (CardInstance cardInstance in cardsHookedUpTo)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);

            cardView.OnBeginDragNotification += CardViewDraggableImageOnBeginDragNotification;
            cardView.OnDragNotification += CardViewOnOnDragNotification;
            cardView.OnExitDragNotification += CardViewDraggableImageOnExitDragNotification;
        }

        List<ActorInstance> actors = battleInstance.allEnemies;
        actors.Add(battleInstance.Player);
        
        foreach (ActorInstance enemy in actors)
        {
            ActorView enemyView = fightView.slotsView.FindActorView(enemy);
            enemyView.OnMouseOverEvent += ActorViewOnOnMouseOverEvent;
            enemyView.OnMouseExitEvent += ActorViewOnOnMouseExitEvent;
        }

        HightlightDefaultState();
    }

    public void Terminate()
    {
        UnhookEvents();
    }

    private void CardViewOnOnDragNotification(CardView obj)
    {
        obj.transform.position = Input.mousePosition;
    }

    private void HightlightDefaultState()
    {
        fightView.uiView.ShowHand(battleInstance.Player.inventory.deck.hand);
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
            OnCompleted?.Invoke();
        }
        else
        {
            HightlightDefaultState();
        }
    }
}