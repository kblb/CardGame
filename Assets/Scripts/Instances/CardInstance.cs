using System;
using Builders;

public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;
    public event Action<ActorInstance> OnCast;

    public CardInstance(CardScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }

    // public void Cast(ActorInstance owner, BattleInstance battleInstance)
    // {
    //     //sleep card doesn't have any action
    //     ActorInstance target = scriptableObject.cardAction?.GetTarget(owner, battleInstance);
    //     OnCast?.Invoke(target);
    //     scriptableObject.cardAction?.Cast(owner, battleInstance);
    // }
    //
    public void AppendToAttack(AttackBuilder attack, ActorInstance owner, BattleInstance battleInstance)
    {
        //sleep card doesn't have any action
        scriptableObject.cardAction?.AppendToAttack(attack, owner, battleInstance);
    }
}