using System;
using System.Collections.Generic;

public class IntentInstance
{
    public readonly ActorInstance owner;
    public readonly ActorInstance target;
    public readonly List<CardInstance> cards = new List<CardInstance>();
    
    public event Action<IntentInstance> OnCast;
    
    public IntentInstance(ActorInstance owner, CardInstance card, ActorInstance target)
    {
        this.owner = owner;
        this.target = target;
        cards.Add(card);
    }

    public void Cast()
    {
        foreach (CardInstance cardInstance in cards)
        {
            cardInstance.CastOn(owner, target);
        }
        OnCast?.Invoke(this);
    }
}