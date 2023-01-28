using System;
using System.Collections.Generic;
using System.Linq;

public class DeckInstance
{
    public readonly List<CardInstance> discardPile = new();
    public readonly List<CardInstance> drawPile = new();
    public readonly List<CardInstance> hand = new();
    public readonly List<CardInstance> intents = new();
    public readonly List<CardInstance> usedEtherealPile = new();

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
            throw new Exception("Hand is empty, can't draw");
        }

        int indexToDraw = drawPile.Count - 1;

        CardInstance card = drawPile[indexToDraw];
        drawPile.RemoveAt(indexToDraw);
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

        IEnumerable<CardInstance> ienumerableDiscardPile = discardPile;
        foreach (CardInstance cardInstance in ienumerableDiscardPile.Reverse())
        {
            drawPile.Add(cardInstance);
        }

        discardPile.Clear();
        OnDrawPileReshuffled?.Invoke();
    }

    private void DiscardCard(CardInstance cardInstance)
    {
        intents.Remove(cardInstance);
        if (cardInstance.scriptableObject.ethereal)
        {
            usedEtherealPile.Add(cardInstance);
        }
        else
        {
            discardPile.Add(cardInstance);
        }

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
        //sleep card doesn't have any action
        cardInstance.scriptableObject.cardAction?.Cast(owner, battleInstance);
        DiscardCard(cardInstance);
    }
}