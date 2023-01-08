using System.Collections.Generic;
using Enemies.Passives.Effects;
using Players;
using Registries;
using UnityEngine;

namespace Enemies.Passives
{
    public class ShieldFront : IEnemyPassive
    {
        public readonly int Amount = 5;
        public readonly int Count = 1;

        public Dictionary<int, IEnemyPassiveEffect> Passive(
            PlayerModel playerModel,
            EnemyModel[] enemies,
            int myEnemyIndex)
        {
            var effects = new Dictionary<int, IEnemyPassiveEffect>();
            for (var i = myEnemyIndex - 1; i >= 0 && i >= myEnemyIndex - Count; i--)
            {
                effects.Add(i, new ShieldBuffEffect(Amount, BattleIconRegistry.ShieldIcon));
                Debug.Log($"Applying passive shield {Amount} to player {i}");
            }
            return effects;
        }
    }
}