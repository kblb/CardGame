using Enemies;
using Players;

namespace Cards.Effects
{
    public class Execute : ICardEffect
    {
        public float HealthThreshold;
        
        public void Apply(PlayerModel player, EnemyModelInstance[] enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.CurrentHealth <= HealthThreshold)
                {
                    enemy.CurrentHealth = 0;
                }
            }
        }
    }
}