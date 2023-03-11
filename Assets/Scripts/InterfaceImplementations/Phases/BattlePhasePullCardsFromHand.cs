using System;
using UnityEngine;

public class BattlePhasePullCardsFromHand : IBattlePhase
{
    private readonly DeckInstance deck;
    private readonly int handSize;
    private readonly LogicQueue logicQueue;

    public BattlePhasePullCardsFromHand(DeckInstance deck, int handSize, LogicQueue logicQueue)
    {
        this.deck = deck;
        this.handSize = handSize;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Pull Cards From Hand");
        int count = handSize - deck.hand.Count;

        int? reshuffleAtIndex = null;
        if (count > deck.drawPile.Count)
        {
            reshuffleAtIndex = deck.drawPile.Count;
        }

        for (int i = 0; i < count; i++)
        {
            if (reshuffleAtIndex.HasValue && reshuffleAtIndex == i)
            {
                logicQueue.AddElement(0.5f, () =>
                {
                    deck.ReshuffleDeck();
                });
            }
            logicQueue.AddElement(0.2f, () =>
            {
                deck.DrawCard();
            });
        }

        logicQueue.AddElement(0.1f, () =>
        {
            OnFinish?.Invoke();
        });
    }
}