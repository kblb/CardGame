using System;
using UnityEngine;

public class SelectCardAndMoveUpPlayerPhase : IPlayerPhase
{
    private readonly FightView fightView;
    private readonly BattleInstance battleInstance;
    
    public ActorView selectedTarget;
    public CardView selectedCard;

    public event Action OnCompleted;

    public SelectCardAndMoveUpPlayerPhase(FightView fightView, BattleInstance battleInstance)
    {
        this.fightView = fightView;
        this.battleInstance = battleInstance;
    }

    public void Start()
    {
        //hooking events
        foreach (CardInstance cardInstance in battleInstance.Player.deck.hand)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            
            cardView.OnBeginDragNotification += CardViewDraggableImageOnBeginDragNotification;
            cardView.OnDragNotification += CardViewOnOnDragNotification;
            cardView.OnExitDragNotification += CardViewDraggableImageOnExitDragNotification;
        }

        foreach (ActorView actorView in fightView.slotsView.actorViews)
        {
            actorView.OnMouseOverEvent += ActorViewOnOnMouseOverEvent;
            actorView.OnMouseExitEvent += ActorViewOnOnMouseExitEvent;
        }

        HightlightDefaultState();
    }

    private void InvokeOnCompleted()
    {
        //unhook events
        foreach (CardInstance cardInstance in battleInstance.Player.deck.hand)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            cardView.OnBeginDragNotification -= CardViewDraggableImageOnBeginDragNotification;
            cardView.OnDragNotification -= CardViewOnOnDragNotification;
            cardView.OnExitDragNotification -= CardViewDraggableImageOnExitDragNotification;
        }

        foreach (ActorView actorView in fightView.slotsView.actorViews)
        {
            actorView.OnMouseOverEvent -= ActorViewOnOnMouseOverEvent;
            actorView.OnMouseExitEvent -= ActorViewOnOnMouseExitEvent;
        }

        //turn off all highlights
        fightView.slotsView.TurnOffHighlight(battleInstance.allEnemies);
        fightView.uiView.TurnOffHighlights();

        OnCompleted?.Invoke();
    }
    
    private void CardViewOnOnDragNotification(CardView obj)
    {
        obj.transform.position = Input.mousePosition;
    }

    private void HightlightDefaultState()
    {
        
        fightView.uiView.ShowHand(battleInstance.Player.deck.hand);
        fightView.uiView.Highlight(battleInstance.Player.deck.hand);
        foreach (ActorView actorView in fightView.slotsView.actorViews)
        {
            actorView.ResetHighlight();
        }
    }

    private void ActorViewOnOnMouseOverEvent(ActorView actorView)
    {
        if (selectedTarget != actorView)
        {
            selectedTarget = actorView;
            actorView.Highlight();
        }
    }

    private void ActorViewOnOnMouseExitEvent(ActorView actorView)
    {
        selectedTarget = null;
        actorView.TurnOffHighlight();
    }

    private void CardViewDraggableImageOnBeginDragNotification(CardView cardView)
    {
        selectedCard = cardView;
        fightView.uiView.TurnOffHighlights();
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