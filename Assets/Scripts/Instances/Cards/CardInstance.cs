public abstract class CardInstance
{
    public readonly BaseCardScriptableObject baseScriptableObject;

    protected CardInstance(BaseCardScriptableObject scriptableObject)
    {
        this.baseScriptableObject = scriptableObject;
    }
}
