using System.Collections.Generic;
using Players;

namespace Enemies
{
    public interface IEnemyPassive
    {
        Dictionary<int, IEnemyPassiveEffect> Passive(PlayerModel playerModel, EnemyModel[] enemies, int myEnemyIndex);
    }
}