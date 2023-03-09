public class CastInstance
{
    public ActorInstance owner;
    public ActorInstance target;
    public int damage;
    
    
    public void Cast()
    {
        target.ReceiveDamage(damage);
    }
}
