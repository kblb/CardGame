using System;
using System.Linq;
using Builders;

public class CardActionTargetOneBehind : ICardAction
{
    public void AppendToAttack(AttackBuilder builder, ActorInstance owner, BattleInstance battleInstance)
    {
        builder.AddTargetRelative();
    }
}