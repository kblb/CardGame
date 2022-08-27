using System.Collections.Generic;
using Cards;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] private EnemyView enemyView;
        [SerializeField] [SceneObjectsOnly] private EnemyQueue enemyQueue;

        [Button]
        public void SpawnEnemy(Enemy enemy)
        {
            var instance = Instantiate(enemyView, enemyQueue.transform);
            instance.Init(enemy);
            enemyQueue.AddEnemy(instance);
        }
        
        public void Attack(List<Card> cards)
        {
            enemyQueue.Attack(cards);
        }
    }
}