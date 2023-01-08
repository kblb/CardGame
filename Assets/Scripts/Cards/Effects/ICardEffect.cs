using Enemies;
using Players;

namespace Cards.Effects
{
    public interface ICardEffect
    {
        void Apply(PlayerModel player, EnemyModelInstance[] enemies);

        Intent Intents();
    }
}