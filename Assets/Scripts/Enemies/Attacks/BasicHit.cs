using Players;

namespace Enemies.Attacks
{
    public class BasicHit : IEnemyAttack
    {
        public float Damage;

        public Attack NextAttack(Player player, Enemy[] allEnemies, int myEnemyIndex)
        {
            return new Attack(Damage, null, null);
        }
    }
}