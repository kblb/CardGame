using UnityEngine;

namespace Enemies.Passives.Effects
{
    public interface IEnemyPassiveEffect
    {
        Sprite Icon { get; }

        void ApplyEffect(EnemyController enemy);
    }
}