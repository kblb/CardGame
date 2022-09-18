using Enemies.Attacks.Effects;
using UnityEngine;

namespace Enemies
{
    public class Attack
    {

        public Attack(float damage, IEnemyAttackEffect effect, Sprite icon)
        {
            Damage = damage;
            Effect = effect;
            Icon = icon;
        }
        public float Damage { get; }
        public IEnemyAttackEffect Effect { get; }
        public Sprite Icon { get; }
    }

}