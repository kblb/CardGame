public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;

    public CardInstance(CardScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }

    public void CastOn(ActorInstance owner, ActorInstance target)
    {
        //sleep does nothing, so cards can have no action at all
        scriptableObject.cardAction?.Cast(owner, target);
    }
}