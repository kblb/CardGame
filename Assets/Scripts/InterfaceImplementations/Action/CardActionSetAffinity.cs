using Builders;
namespace InterfaceImplementations.Action
{
    public class CardActionSetAffinity : ICardAction
    {
        public Affinity affinity;
        
        public void AppendToAttack(AttackBuilder builder, ActorInstance owner, BattleInstance battleInstance)
        {
            builder.AppendAffinity(affinity);
        }
    }
}