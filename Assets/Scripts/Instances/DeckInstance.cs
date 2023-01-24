using System;
using System.Collections.Generic;

public class DeckInstance
{
    public readonly List<CardInstance> discardPile = new();
    public readonly List<CardInstance> drawPile = new();
    public readonly List<CardInstance> hand = new();
    public readonly List<CardInstance> intents = new();

    public event Action<CardInstance> OnNewCardDrawn;
    public event Action<CardInstance> OnCardDiscarded;
    public event Action OnDrawPileReshuffled;
    public event Action<CardInstance> OnCardAddedToHand;
    public event Action OnIntentUpdated;

    public DeckInstance(IEnumerable<CardScriptableObject> cards)
    {
        foreach (CardScriptableObject cardScriptableObject in cards)
        {
            drawPile.Add(new CardInstance(cardScriptableObject));
        }
    }

    public CardInstance DrawCard()
    {
        if (drawPile.Count == 0)
        {
            ReshuffleDeck();
        }
        CardInstance card = drawPile[0];
        drawPile.RemoveAt(0);
        hand.Add(card);
        OnNewCardDrawn?.Invoke(card);
        return card;
    }

    public void ReshuffleDeck()
    {
        if (discardPile.Count == 0)
        {
            throw new Exception("Can't reshuffle, because discard pile is empty");
        }
        foreach (CardInstance cardInstance in discardPile)
        {
            drawPile.Add(cardInstance);
        }

        discardPile.Clear();
        OnDrawPileReshuffled?.Invoke();
    }

    public void DiscardCard(CardInstance cardInstance)
    {
        intents.Remove(cardInstance);
        discardPile.Add(cardInstance);
        OnCardDiscarded?.Invoke(cardInstance);
    }

    public void AddCardToCommitArea(CardInstance cardInstance)
    {
        hand.Remove(cardInstance);
        intents.Add(cardInstance);
        OnIntentUpdated?.Invoke();
    }

    public void RemoveCardFromCommitArea(CardInstance cardInstance)
    {
        intents.Remove(cardInstance);
        hand.Add(cardInstance);
        OnCardAddedToHand?.Invoke(cardInstance);
        OnIntentUpdated?.Invoke();
    }

    public void OnIntentReadyInvoke()
    {
        OnIntentUpdated?.Invoke();
    }

    public void Cast(CardInstance cardInstance, ActorInstance owner, BattleInstance battleInstance)
    {
        cardInstance.scriptableObject.cardAction.Cast(owner, battleInstance);
        DiscardCard(cardInstance);
    }
}