﻿using System;

public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;
    public event Action<ActorInstance> OnCast;

    public CardInstance(CardScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }

    public void Cast(ActorInstance owner, BattleInstance battleInstance)
    {
        //sleep card doesn't have any action
        ActorInstance target = scriptableObject.cardAction?.GetTarget(owner, battleInstance);
        scriptableObject.cardAction?.Cast(owner, battleInstance);
        OnCast?.Invoke(target);
    }
}