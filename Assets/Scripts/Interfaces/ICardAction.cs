using Builders;

public interface ICardAction
{
    void AppendToAttack(AttackBuilder builder, ActorInstance owner, BattleInstance battleInstance);
}