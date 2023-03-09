using System;
using System.Linq;
using Builders;

public class CardActionTargetAllEnemies : ICardAction
{
    public void AppendToAttack(AttackBuilder builder, ActorInstance owner, BattleInstance battleInstance)
    {
        builder.AddTargets(new[] { 0, 1, 2, 3, 4 });
    }
}