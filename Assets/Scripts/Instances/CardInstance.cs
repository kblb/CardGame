using System;

public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;

    public event Action OnCast;
    
    public CardInstance(CardScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }

    public void CastOn(FightPhaseActorInstance first)
    {
        scriptableObject.cardAction.CastOn(first);
        OnCast?.Invoke();
    }
}
