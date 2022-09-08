using System.Collections.Generic;
using Cards;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private EnemyQueue enemyQueue;

        [Button]
        public void SpawnEnemy(Enemy enemy)
        {
            enemyQueue.AddEnemy(enemy);
        }
        
        public void Attack(List<Card> cards)
        {
            enemyQueue.Attack(cards);
        }
    }
}