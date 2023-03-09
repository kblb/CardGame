
public class DealDamageCast : ICast
{
    public int amount;

    public CastInstance CreateCastInstance(ActorInstance owner, ActorInstance target)
    {
        return new CastInstance()
        {
            owner = owner,
            target = target,
            damage = amount
        };
    }
}