﻿using System;

public class FightPhasePullCardsFromHand : IFightPhase
{
    private readonly DeckInstance deck;
    private readonly int handSize;
    private readonly LogicQueue logicQueue;

    public FightPhasePullCardsFromHand(DeckInstance deck, int handSize, LogicQueue logicQueue)
    {
        this.deck = deck;
        this.handSize = handSize;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        for (int i = deck.hand.Count; i < handSize; i++)
        {
            logicQueue.AddElement(() => { deck.DrawCard(); });
        }

        logicQueue.AddElement(() => { OnFinish?.Invoke(); });
    }
}