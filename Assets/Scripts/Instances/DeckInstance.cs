using System;
using System.Collections.Generic;

public class DeckInstance
{
    public readonly List<CardInstance> discardPile = new();
    public readonly List<CardInstance> drawPile = new();
    public readonly List<CardInstance> hand = new();
    public readonly List<CardInstance> intents = new();

    public event Action<CardInstance> NewCardDrawn;
    public event Action<CardInstance> NewCardDiscarded;
    public event Action DrawPileReshuffled;
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
        NewCardDrawn?.Invoke(card);
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
        DrawPileReshuffled?.Invoke();
    }

    public void DiscardCard(CardInstance cardInstance)
    {
        hand.Remove(cardInstance);
        discardPile.Add(cardInstance);
        NewCardDiscarded?.Invoke(cardInstance);
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
}