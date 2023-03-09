public class AttackCardInstance : CardInstance
{
    public readonly AttackCardScriptableObject scriptableObject;

    public AttackCardInstance(AttackCardScriptableObject scriptableObject) : base(scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }
}