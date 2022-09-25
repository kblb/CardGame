using Enemies;
using Players;

namespace Cards.Effects
{
    public class Execute : ICardEffect
    {
        public float[] HealthThreshold;

        public void Apply(PlayerModel player, EnemyModelInstance[] enemies)
        {
            for (var i = 0; i < enemies.Length && i < HealthThreshold.Length; i++)
            {
                if (HealthThreshold[i] < 0) continue;

                var enemy = enemies[i];
                if (enemy.CurrentHealth <= HealthThreshold[i]) enemy.CurrentHealth = 0;
            }
        }
    }
}