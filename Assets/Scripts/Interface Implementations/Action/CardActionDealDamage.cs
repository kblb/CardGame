
public class CardActionDealDamage : ICardAction
{
    public int amount;

    public void Cast(ActorInstance owner, ActorInstance target)
    {
        target.ReceiveDamage(amount);
    }
}