using System;
using System.Collections.Generic;
using System.Linq;

public class DeckInstance
{
    public IntentInstance intent;
    public readonly List<CardInstance> discardPile = new();
    public readonly List<CardInstance> drawPile = new();
    public readonly List<CardInstance> hand = new();
    public readonly List<CardInstance> usedEtherealPile = new();

    public event Action<CardInstance> OnCardDiscarded;
    public event Action OnDrawPileReshuffled;
    public event Action OnIntentUpdated;
    public event Action OnCardRemovedFromHand;
    public event Action<CardInstance> OnCardAddedToHand;
    public event Action<CardInstance> OnCardRemovedFromDrawPile;

    public DeckInstance(IEnumerable<BaseCardScriptableObject> cards)
    {
        foreach (BaseCardScriptableObject cardScriptableObject in cards)
        {
            if (cardScriptableObject is AttackCardScriptableObject scriptableObject)
            {
                drawPile.Add(new AttackCardInstance(scriptableObject));
            }
            else if (cardScriptableObject is ModifierCardScriptableObject modifierCardScriptableObject)
            {
                drawPile.Add(new ModifyCardInstance(modifierCardScriptableObject));
            }
            else
            {
                throw new Exception("Unknown card scriptable object type");
            }
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
        foreach (CardInstance cardInstance in intent.GetAllCards())
        {
            if (cardInstance.baseScriptableObject.isEthereal)
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
        
        foreach (CardInstance card in intent.GetAllCards())
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

    public void AddCardToIntent(ModifyCardInstance cardInstance)
    {
        hand.Remove(cardInstance);
        intent.modifiers.Add(cardInstance);
        OnIntentUpdated?.Invoke();
        OnCardRemovedFromHand?.Invoke();
    }
}