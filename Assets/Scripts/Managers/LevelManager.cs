using Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Level currentLevel;
        [ReadOnly] [SerializeField]
        private int currentEnemyIndex;
        [SerializeField] [SceneObjectsOnly]
        private EnemyManager enemyManager;
        private AnimationQueue animationQueue;

        private void ReplenishEnemies()
        {
            var toSpawn = 5 - enemyManager.EnemyCount();
            for (var i = 0; i < toSpawn; i++)
            {
                var enemy = currentLevel.Get(i + currentEnemyIndex);
                if (enemy != null)
                {
                    animationQueue.AddElement(() =>
                    {
                        enemyManager.SpawnEnemy(enemy);
                    });
                }
                else
                {
                    Debug.LogWarning("Could not spawn enemy at index " + (i + currentEnemyIndex));
                }
            }
            currentEnemyIndex += toSpawn;
        }

        public void Init(AnimationQueue animationQueue1)
        {
            this.animationQueue = animationQueue1;
            ReplenishEnemies();
            enemyManager.RegisterOnEnemyKilled(ReplenishEnemies);
        }
    }
}