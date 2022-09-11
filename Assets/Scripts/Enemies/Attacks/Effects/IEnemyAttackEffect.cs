using Players;

namespace Enemies
{
    public interface IEnemyAttackEffect
    {
        void Apply(PlayerModel playerModel, EnemyController[] allEnemies, int myEnemyIndex);
    }
}