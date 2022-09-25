using Enemies;
using Players;

namespace Cards.Effects
{
    public class Damage : ICardEffect
    {
        public float[] Amount;

        public void Apply(PlayerModel player, EnemyModelInstance[] enemies)
        {
            for (var i = 0; i < enemies.Length && i < Amount.Length; i++) enemies[i].Damage(Amount[i]);
        }
    }
}