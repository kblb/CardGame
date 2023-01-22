public class CardActionDealDamage : ICardAction
{
    public int amount;
    public void CastOn(FightPhaseActorInstance actor)
    {
        actor.ReceiveDamage(amount);
    }
}