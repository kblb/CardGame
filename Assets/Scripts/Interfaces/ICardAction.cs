public interface ICardAction
{
    void Cast(ActorInstance owner, BattleInstance battleInstance);
    ActorInstance GetTarget(ActorInstance owner, BattleInstance battleInstance);
}