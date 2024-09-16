using System;
using UnityEngine;

public class IntentInstance
{
    public readonly ActorInstance owner;
    public readonly ActorInstance targetActor;

    public readonly CardInstance card;
    
    public IntentInstance(ActorInstance owner, CardInstance cardCard, ActorInstance targetActor)
    {
        this.owner = owner;
        this.targetActor = targetActor;

        card = cardCard;
    }

    public void Cast(BattleInstance battleInstance)
    {
        //sleep card doesn't have any casts
        if (card.scriptableObject.cast != null)
        {
            CastInstance castInstance = card.scriptableObject.cast.CreateCastInstance(owner, targetActor);

            castInstance.Cast();
        }
    }
}