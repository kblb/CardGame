using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyQueue : MonoBehaviour
    {
        [SerializeField, SceneObjectsOnly]
        private List<Transform> slots;
        private List<EnemyView> enemies;
        [SerializeField, AssetsOnly]
        private EnemyView enemyView;

        private void Awake()
        {
            enemies = new List<EnemyView>();
        }

        public bool AddEnemy(Enemy enemy)
        {
            return AddToNextFreeSlot(enemy);
        }
        
        private bool AddToNextFreeSlot(Enemy enemy)
        {
            if (enemies.Count >= slots.Count) return false;
            
            var instance = Instantiate(enemyView, slots[enemies.Count]);
            instance.Init(enemy);
            enemies.Add(instance);

            return true;
        }

        public void Attack(List<Card> cards)
        {
            foreach (var card in cards)
            {
                var enemy = enemies.First();
                if (enemy.Attack(card))
                {
                    enemies.RemoveAt(0);
                    Destroy(enemy.gameObject);
                }
            }
            Redraw();
        }
        
        private void Redraw()
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                enemies[i].transform.SetParent(slots[i]);
                enemies[i].transform.localPosition = Vector3.zero;
            }
        }
    }
}