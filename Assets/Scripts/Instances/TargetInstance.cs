
using System.Collections.Generic;

public class TargetInstance
{
    public List<ActorInstance> actors = new List<ActorInstance>();

    public TargetInstance(ActorInstance target)
    {
        actors.Add(target);
    }

    public void ReceiveDamage(int damage)
    {
        foreach (ActorInstance actor in actors)
        {
            actor.ReceiveDamage(damage);
        }
    }
}