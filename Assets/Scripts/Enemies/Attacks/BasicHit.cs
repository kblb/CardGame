using Players;

namespace Enemies.Attacks
{
    public class BasicHit : IEnemyAttack
    {
        public float Damage;

        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            return new Attack(Damage, null, null);
        }
    }
}