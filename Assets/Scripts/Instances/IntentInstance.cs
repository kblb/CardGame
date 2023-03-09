using System;
using System.Collections.Generic;

public class IntentInstance
{
    public readonly ActorInstance owner;
    public readonly ActorInstance targetActor;

    public readonly AttackCardInstance attack;
    public readonly List<ModifyCardInstance> modifiers = new List<ModifyCardInstance>();
    
    public event Action<IntentInstance> OnCast;
    
    public IntentInstance(ActorInstance owner, AttackCardInstance attackCard, ActorInstance targetActor)
    {
        this.owner = owner;
        this.targetActor = targetActor;

        attack = attackCard;
    }

    public void Cast()
    {
        //sleep card doesn't have any casts
        if (attack.scriptableObject.cast != null)
        {
            CastInstance castInstance = attack.scriptableObject.cast.CreateCastInstance(owner, targetActor);

            foreach (ModifyCardInstance modifyCardInstance in modifiers)
            {
                modifyCardInstance.scriptableObject.modify.Modify(castInstance);
            }

            castInstance.Cast();
        }

        OnCast?.Invoke(this);
    }

    public IEnumerable<CardInstance> GetAllCards()
    {
        return new List<CardInstance>(modifiers)
        {
            attack
        };
    }
}