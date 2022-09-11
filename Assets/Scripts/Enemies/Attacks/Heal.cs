using Enemies.Attacks.Effects;
using Players;

namespace Enemies.Attacks
{
    public class Heal : IEnemyAttack
    {
        public readonly float FlatHealAmount;
        public readonly float NoHealDamage;

        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            return myEnemyIndex switch
            {
                > 0 => new Attack(0, new HealEnemyInFront(FlatHealAmount), null),
                _ => new Attack(NoHealDamage, null, null)
            };
        }
    }
}