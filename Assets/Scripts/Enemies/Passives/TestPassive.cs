using System.Collections.Generic;
using Enemies.Passives.Effects;
using Players;

namespace Enemies.Passives
{
    public class TestPassive : IEnemyPassive
    {
        public Dictionary<int, IEnemyPassiveEffect> Passive(PlayerModel playerModel, EnemyModel[] enemies, int myEnemyIndex)
        {
            return new Dictionary<int, IEnemyPassiveEffect>();
        }
    }
}