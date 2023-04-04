using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DragJewelsToCardsPlayerPhase : IPlayerPhase
{
    private readonly BattleInstance battleInstance;
    private readonly FightView fightView;
    public event Action OnCancel;
    public event Action OnCompleted;

    private List<CardView> cardsInHand = new List<CardView>();

    private CardView selectedCard;
    private JewelView selectedJewel;

    public DragJewelsToCardsPlayerPhase(BattleInstance battleInstance, FightView fightView)
    {
        this.battleInstance = battleInstance;
        this.fightView = fightView;
    }

    public void Start()
    {
        foreach (JewelInstance jewelInstance in battleInstance.Player.inventory.jewels)
        {
            JewelView jewelView = fightView.uiView.FindJewelView(jewelInstance);
            jewelView.OnDragNotification += JewelViewOnOnDragNotification;
            jewelView.OnExitDragNotification += JewelViewOnOnExitDragNotification;
        }

        foreach (CardInstance cardInstance in battleInstance.Player.inventory.deck.hand)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            cardsInHand.Add(cardView);
        }

        OnCompleted += UnhookEvents;
    }

    private void JewelViewOnOnExitDragNotification(JewelView obj)
    {
        if (selectedCard == null)
        {
            fightView.uiView.jewelsFrame.ReturnAllJewelsToSlots();
        }
        else
        {
            selectedCard.cardInstance.InsertJewel(selectedJewel.instance);
            battleInstance.Player.inventory.RemoveJewel(selectedJewel.instance);
        }
    }

    private void UnhookEvents()
    {
        foreach (JewelInstance jewelInstance in battleInstance.Player.inventory.jewels)
        {
            JewelView jewelView = fightView.uiView.FindJewelView(jewelInstance);
            jewelView.OnDragNotification -= JewelViewOnOnDragNotification;
        }
    }

    private void JewelViewOnOnDragNotification(JewelView obj)
    {
        Vector3 mousePos = Input.mousePosition;
        obj.transform.DOMove(mousePos, .2f);

        selectedJewel = obj;

        CardView newCardView = CardView.IsPointerOverType<CardView>();

        if (selectedCard != null)
        {
            selectedCard.RestoreToOriginalPosition();
        }

        if (newCardView != null)
        {
            newCardView.MoveUp();
        }

        selectedCard = newCardView;
    }

    public void Terminate()
    {
        UnhookEvents();
    }
}