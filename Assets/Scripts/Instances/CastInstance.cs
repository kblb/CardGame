using System.Collections.Generic;

public class CastInstance
{
    public ActorInstance owner;
    public List<ActorInstance> targets = new List<ActorInstance>();
    public int damage;


    public void Cast()
    {
        foreach (ActorInstance target in targets)
        {
            target.ReceiveDamage(damage);
        }
    }
}