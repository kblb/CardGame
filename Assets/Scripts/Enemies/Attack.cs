using Players;
using UnityEngine;

namespace Enemies
{
    public struct Attack
    {
        public float Damage { get; }
        public IEnemyAttackEffect Effect { get; }
        public Sprite Icon { get; }

        public Attack(float damage, IEnemyAttackEffect effect, Sprite icon)
        {
            Damage = damage;
            Effect = effect;
            Icon = icon;
        }

    }

    public interface IEnemyAttackEffect
    {
        void Apply(PlayerModel playerModel, Enemy[] allEnemies, int myEnemyIndex);
    }
}