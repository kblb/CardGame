using System;
using System.Collections.Generic;

public class DeckInstance
{
    public List<CardInstance> discardPile = new();
    public List<CardInstance> drawPile = new();
    public List<CardInstance> hand = new();
    public List<CardInstance> commitArea = new();

    public event Action<CardInstance> NewCardDrawn;
    public event Action<CardInstance> NewCardDiscarded;
    public event Action DrawPileReshuffled;
    public event Action<CardInstance> OnCardAddedToCommitArea;
    public event Action<CardInstance> OnCardAddedToHand;

    public DeckInstance(IEnumerable<CardScriptableObject> cards)
    {
        foreach (CardScriptableObject cardScriptableObject in cards)
        {
            drawPile.Add(new CardInstance(cardScriptableObject));
        }
    }

    public void DrawCard()
    {
        CardInstance card = drawPile[0];
        drawPile.RemoveAt(0);
        hand.Add(card);
        NewCardDrawn?.Invoke(card);
    }

    public void ReshuffleDeck()
    {
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
        commitArea.Add(cardInstance);
        OnCardAddedToCommitArea?.Invoke(cardInstance);
    }

    public void AddCardToHand(CardInstance cardInstance)
    {
        commitArea.Remove(cardInstance);
        hand.Add(cardInstance);
        OnCardAddedToHand?.Invoke(cardInstance);
    }
}