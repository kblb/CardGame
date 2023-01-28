using System.Linq;

public class CardActionDealDamage : ICardAction
{
    public int amount;

    public void Cast(ActorInstance owner, BattleInstance battleInstance)
    {
        ActorInstance target = null;
        if (battleInstance.allEnemies.Contains(owner))
        {
            target = battleInstance.Player;
        }
        else if (battleInstance.Player == owner)
        {
            target = battleInstance.allEnemies.First();
        }

        target.ReceiveDamage(amount);
    }

    public ActorInstance GetTarget(ActorInstance owner, BattleInstance battleInstance)
    {
        return battleInstance.slots.First(t => t.actor != null).actor;
    }
}