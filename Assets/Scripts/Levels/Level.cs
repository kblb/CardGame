using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(menuName = "Create/Level")]
    public class Level : SerializedScriptableObject
    {
        public List<EnemyModel> listOfEnemies = new();
        public EnemyModel Get(int index) => listOfEnemies[index];
    }
}