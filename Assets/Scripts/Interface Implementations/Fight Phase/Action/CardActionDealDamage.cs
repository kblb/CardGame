public class CardActionDealDamage : ICardAction
{
    public int amount;

    public void Act(ActorInstance target)
    {
        target.ReceiveDamage(amount);
    }
}