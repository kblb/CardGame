using System;

public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;

    public CardInstance(CardScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }

    public ActionInstance CreateActionInstance(ActorInstance target)
    {
        return new ActionInstance(scriptableObject, target);
    }
}
