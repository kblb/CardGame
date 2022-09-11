using Players;

namespace Enemies.Attacks
{
    public class TestAttack : IEnemyAttack
    {
        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            return new Attack(10, null, null);
        }
    }
}