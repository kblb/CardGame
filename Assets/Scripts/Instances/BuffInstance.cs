using Builders;
using Interfaces;

public class BuffInstance
{
    public readonly BuffScriptableObject scriptableObject;
    public int amount;

    public BuffInstance(BuffScriptableObject scriptableObject, int amount = 1)
    {
        this.scriptableObject = scriptableObject;
        this.amount = amount;
    }

    public int AlterDamageReceived(int attackAmount, Affinity attackAffinity)
    {
        return scriptableObject.buff.AlterDamageReceived(attackAmount, attackAffinity);
    }
}