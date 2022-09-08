using Players;

namespace Enemies.Attacks.Effects
{
    public class HealEnemyInFront : IEnemyAttackEffect
    {
        private readonly float _flatHealAmount;

        public HealEnemyInFront(float flatHealAmount)
        {
            _flatHealAmount = flatHealAmount;
        }

        public void Apply(PlayerModel playerModel, Enemy[] allEnemies, int myEnemyIndex)
        {
            if (myEnemyIndex > 0) allEnemies[myEnemyIndex - 1].Heal(_flatHealAmount);
        }
    }
}