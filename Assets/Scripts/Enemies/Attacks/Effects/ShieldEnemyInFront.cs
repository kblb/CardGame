using Players;

namespace Enemies.Attacks.Effects
{
    public class ShieldEnemyInFront : IEnemyAttackEffect
    {
        private readonly float _flatShieldAmount;

        public ShieldEnemyInFront(float flatShieldAmount)
        {
            _flatShieldAmount = flatShieldAmount;
        }

        public void Apply(PlayerModel playerModel, EnemyController[] allEnemies, int myEnemyIndex)
        {
            if (myEnemyIndex > 0) allEnemies[myEnemyIndex - 1].Shield(_flatShieldAmount);
        }
    }
}