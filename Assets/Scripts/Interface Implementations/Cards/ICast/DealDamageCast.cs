using System.Collections.Generic;

public class DealDamageCast : ICast
{
    public int amount;

    public CastInstance CreateCastInstance(ActorInstance owner, ActorInstance target)
    {
        return new CastInstance()
        {
            owner = owner,
            targets = new List<ActorInstance>()
            {
                target
            },
            damage = amount
        };
    }
}