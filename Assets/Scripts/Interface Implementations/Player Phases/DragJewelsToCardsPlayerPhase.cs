using System;
using System.Collections.Generic;
using UnityEngine;

public class DragJewelsToCardsPlayerPhase : IPlayerPhase
{
    private readonly BattleInstance battleInstance;
    private readonly FightView fightView;
    public event Action OnCancel;
    public event Action OnCompleted;

    private List<CardView> cardsInHand = new List<CardView>();

    private CardView selectedCard;

    public DragJewelsToCardsPlayerPhase(BattleInstance battleInstance, FightView fightView)
    {
        this.battleInstance = battleInstance;
        this.fightView = fightView;
    }

    public void Start()
    {
        foreach (CardInstance cardInstance in battleInstance.Player.inventory.deck.hand)
        {
            CardView cardView = fightView.uiView.FindCardView(cardInstance);
            cardsInHand.Add(cardView);
        }

        OnCompleted += UnhookEvents;
    }

    private void UnhookEvents()
    {
    }

    public void Terminate()
    {
        UnhookEvents();
    }
}