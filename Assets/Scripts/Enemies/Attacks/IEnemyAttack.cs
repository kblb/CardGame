using Players;

namespace Enemies.Attacks
{
    public interface IEnemyAttack
    {
        Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex);
    }
}