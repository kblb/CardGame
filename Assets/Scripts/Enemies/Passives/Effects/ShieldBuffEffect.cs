using UnityEngine;

namespace Enemies.Passives.Effects
{
    public class ShieldBuffEffect : IEnemyPassiveEffect
    {
        private readonly int _amount;

        public ShieldBuffEffect(int amount, Sprite icon)
        {
            _amount = amount;
            Icon = icon;
        }
        public Sprite Icon { get; }

        public void ApplyEffect(EnemyController enemy)
        {
            enemy.Shield(_amount);
        }
    }
}