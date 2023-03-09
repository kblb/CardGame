using System.Linq;
using Builders;

public class CardActionBoostDamage : ICardAction
{
    public int amount;

    public void AppendToAttack(AttackBuilder builder, ActorInstance owner, BattleInstance battleInstance)
    {
        var target = battleInstance.allEnemies.Contains(owner) ? AttackBuilder.PLAYER_TARGET_INDEX : 0;
        builder.AddDamage(amount);
    }
}