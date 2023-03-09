public class ModifyCardInstance : CardInstance
{
    public readonly ModifierCardScriptableObject scriptableObject;

    public ModifyCardInstance(ModifierCardScriptableObject cardScriptableObject) : base(cardScriptableObject)
    {
        this.scriptableObject = cardScriptableObject;
    }
}