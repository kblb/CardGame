public class MultiplyAttackModify : IModify
{
    public readonly int multiplier;

    public MultiplyAttackModify(int multiplier)
    {
        this.multiplier = multiplier;
    }
    
    public void Modify(CastInstance castInstance, BattleInstance battleInstance)
    {
        castInstance.damage *= multiplier;
    }
}
