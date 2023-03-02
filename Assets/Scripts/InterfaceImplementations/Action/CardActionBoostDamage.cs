using System.Linq;
using Builders;

public class CardActionBoostDamage : ICardAction
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

        target.ReceiveDamage(amount, Affinity.None);
    }

    public void AppendToAttack(AttackBuilder builder, ActorInstance owner, BattleInstance battleInstance)
    {
        var target = battleInstance.allEnemies.Contains(owner) ? AttackBuilder.PLAYER_TARGET_INDEX : 0;
        builder.AddDamage(amount, target);
    }
}