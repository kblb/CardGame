using Enemies.Attacks.Effects;
using Players;
using Registries;
using UnityEngine;

namespace Enemies.Attacks
{
    public class Shield : IEnemyAttack
    {
        public readonly float FlatShieldAmount;
        public readonly float NoShieldDamage;

        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            return myEnemyIndex switch
            {
                > 0 => new Attack(0, new ShieldEnemyInFront(FlatShieldAmount), BattleIconRegistry.ShieldIcon),
                _ => new Attack(NoShieldDamage, null, BattleIconRegistry.SwordIcon)
            };
        }
    }
}