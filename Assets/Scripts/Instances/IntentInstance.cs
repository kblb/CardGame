using System;
using System.Collections.Generic;

public class IntentInstance
{
    public readonly ActorInstance owner;
    public readonly ActorInstance targetActor;
    public readonly List<CardInstance> cards = new List<CardInstance>();
    
    public event Action<IntentInstance> OnCast;
    
    public IntentInstance(ActorInstance owner, CardInstance card, ActorInstance targetActor)
    {
        this.owner = owner;
        this.targetActor = targetActor;
        cards.Add(card);
    }

    public void Cast()
    {
        foreach (CardInstance cardInstance in cards)
        {
            cardInstance.CastOn(owner, targetActor);
        }
        OnCast?.Invoke(this);
    }
}