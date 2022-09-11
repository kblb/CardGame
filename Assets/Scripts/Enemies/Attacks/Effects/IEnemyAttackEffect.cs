using Players;

namespace Enemies.Attacks.Effects
{
    public interface IEnemyAttackEffect
    {
        void Apply(PlayerModel playerModel, EnemyController[] allEnemies, int myEnemyIndex);
    }
}