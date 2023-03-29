using System.Collections.Generic;

public class CastInstance
{
    public ActorInstance owner;
    public List<ActorInstance> targets = new List<ActorInstance>();
    public int damage;
    public int shields;

    public void Cast()
    {
        foreach (ActorInstance target in targets)
        {
            if (shields > 0)
            {
                target.AddShields(shields);
            }
            
            if (damage > 0)
            {
                target.ReceiveDamage(damage);
            }
        }
    }
}