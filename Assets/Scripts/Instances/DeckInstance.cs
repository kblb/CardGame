using System;
using System.Collections.Generic;
using System.Linq;

public class DeckInstance
{
    public readonly List<CardInstance> discardPile = new();
    public readonly List<CardInstance> drawPile = new();
    public readonly List<CardInstance> hand = new();
    public IntentInstance intent;
    public readonly List<CardInstance> usedEtherealPile = new();

    public event Action<CardInstance> OnCardDiscarded;
    public event Action OnDrawPileReshuffled;
    public event Action OnIntentUpdated;
    public event Action OnCardRemovedFromHand;
    public event Action<CardInstance> OnCardAddedToHand;
    public event Action<CardInstance> OnCardRemovedFromDrawPile;

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
        OnCardRemovedFromDrawPile?.Invoke(card);
        OnCardAddedToHand?.Invoke(card);
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

    public void DiscardIntent(IntentInstance intent)
    {
        foreach (CardInstance cardInstance in intent.cards)
        {
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
        this.intent = null;
    }

    public void AddIntent(IntentInstance intent)
    {
        if (this.intent != null)
        {
            throw new Exception("Will not add intent while there is another one already.");
        }
        
        foreach (CardInstance card in intent.cards)
        {
            if (hand.Contains(card))
            {
                hand.Remove(card);
                OnCardRemovedFromHand?.Invoke();
            }
        }

        this.intent = intent;
        OnIntentUpdated?.Invoke();
    }
}