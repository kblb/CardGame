using Enemies.Attacks.Effects;
using Players;

namespace Enemies.Attacks
{
    public class Heal : IEnemyAttack
    {
        public readonly float NoHealDamage;
        public readonly float FlatHealAmount;
        
        public Attack NextAttack(Player player, Enemy[] allEnemies, int myEnemyIndex)
        {
            return myEnemyIndex switch
            {
                > 0 => new Attack(0, new HealEnemyInFront(FlatHealAmount), null),
                _ => new Attack(NoHealDamage, null, null)
            };
        }
    }
}