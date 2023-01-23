public class ActionInstance
{
    private readonly CardScriptableObject scriptableObject;
    private readonly ActorInstance target;

    public ActionInstance(CardScriptableObject scriptableObject, ActorInstance target)
    {
        this.scriptableObject = scriptableObject;
        this.target = target;
    }

    public void Act()
    {
        scriptableObject.cardAction.Act(target);
    }
}