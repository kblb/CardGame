using System;
using System.Collections.Generic;

public class DeckInstance
{
    public List<CardInstance> discardPile = new();
    public List<CardInstance> drawPile = new();
    public List<CardInstance> hand = new();

    public event Action<CardInstance> NewCardDrawn;
    public event Action<CardInstance> NewCardDiscarded;
    public event Action DrawPileReshuffled;

    public void Init(IEnumerable<CardScriptableObject> cards)
    {
        foreach (CardScriptableObject cardScriptableObject in cards)
        {
            drawPile.Add(new CardInstance(cardScriptableObject));
        }
    }

    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            throw new Exception("Can not draw cards, draw pile is empty. Reshuffle is needed.");
        }

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
}