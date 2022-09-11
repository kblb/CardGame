using System.Collections.Generic;
using Enemies.Passives.Effects;
using Players;

namespace Enemies.Passives
{
    public interface IEnemyPassive
    {
        Dictionary<int, IEnemyPassiveEffect> Passive(PlayerModel playerModel, EnemyModel[] enemies, int myEnemyIndex);
    }
}