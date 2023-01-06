using Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Level currentLevel;
        [ReadOnly, SerializeField]
        private int currentEnemyIndex;
        [SerializeField, SceneObjectsOnly]
        private EnemyManager enemyManager;

        private void Start()
        {
            var toSpawn = 5 - enemyManager.EnemyCount();
            for (int i = 0; i < toSpawn; i++)
            {
                var enemy = currentLevel.Get(i + currentEnemyIndex);
                enemyManager.SpawnEnemy(enemy);
            }
        }
    }
}