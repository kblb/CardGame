public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;

    public CardInstance(CardScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }

    public void CastOn(FightPhaseActorInstance first)
    {
        scriptableObject.cardAction.CastOn(first);
    }
}
