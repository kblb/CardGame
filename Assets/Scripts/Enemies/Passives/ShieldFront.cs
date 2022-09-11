using System.Collections.Generic;
using Enemies.Passives.Effects;
using Players;

namespace Enemies.Passives
{
    public class ShieldFront : IEnemyPassive
    {
        public readonly int Amount = 5;
        public readonly int Count = 1;

        public Dictionary<int, IEnemyPassiveEffect> Passive(PlayerModel playerModel, EnemyModel[] enemies, int myEnemyIndex)
        {
            var effects = new Dictionary<int, IEnemyPassiveEffect>();
            for (var i = myEnemyIndex - 1; i >= 0 && i >= myEnemyIndex - 1 - Count; i--) effects.Add(i, new ShieldBuffEffect(Amount));
            return effects;
        }
    }
}