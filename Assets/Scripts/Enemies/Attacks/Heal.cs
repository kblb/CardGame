using Enemies.Attacks.Effects;
using Players;
using Registries;
using UnityEngine;

namespace Enemies.Attacks
{
    public class Heal : IEnemyAttack
    {
        public readonly float FlatHealAmount;
        public readonly float NoHealDamage;

        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            return myEnemyIndex switch
            {
                > 0 => new Attack(0, new HealEnemyInFront(FlatHealAmount), BattleIconRegistry.HealIcon),
                _ => new Attack(NoHealDamage, null, BattleIconRegistry.SwordIcon)
            };
        }
    }
}