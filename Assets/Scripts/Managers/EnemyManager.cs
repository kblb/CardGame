using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] private EnemyView enemyView;
        [SerializeField] [SceneObjectsOnly] private EnemyQueue enemyQueue;

        public void SpawnEnemy(Enemy enemy)
        {
            var enemyView = Instantiate(this.enemyView, enemyQueue.transform);
            enemyView.Init(enemy);
            //enemyQueue.Add(enemyView);
        }
    }
}