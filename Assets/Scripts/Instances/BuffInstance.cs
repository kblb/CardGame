public class BuffInstance
{
    public readonly BuffScriptableObject scriptableObject;
    public int amount = 1;

    public BuffInstance(BuffScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
    }
}