using System;
using System.Collections.Generic;
using Enemies;

namespace Levels
{
    [Serializable]
    public class Level
    {
        public List<EnemyModel> listOfEnemies = new List<EnemyModel>();
    }
}