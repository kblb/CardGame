using Players;

namespace Enemies
{
    public interface IEnemyAttack
    {
        Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex);
    }
}