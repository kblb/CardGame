public class MultiplyAttackModify : IModify
{
    public readonly int damageToMultiply;

    public MultiplyAttackModify(int damageToMultiply)
    {
        this.damageToMultiply = damageToMultiply;
    }
    
    public void Modify(CastInstance castInstance)
    {
        castInstance.damage *= damageToMultiply;
    }
}
