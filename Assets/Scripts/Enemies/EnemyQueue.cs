using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyQueue : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private List<Transform> slots;
        [SerializeField] [AssetsOnly] private EnemyController enemyControllerPrefab;
        [FormerlySerializedAs("_enemies")] public List<EnemyController> enemies;
        public IEnumerable<EnemyController> Enemies => enemies;

        private void Awake()
        {
            enemies = new List<EnemyController>();
        }
        public event Action OnEnemyKilled;

        public EnemyController AddEnemy(
            EnemyModel enemy,
            EnemyModelInstance enemyModelInstance,
            PlayerModel playerModel)
        {
            var enemyController = AddToNextFreeSlot(enemy, enemyModelInstance);
            if (enemyController != null)
                enemyController.SelectNextAttack(playerModel, enemies.Select(x => x.RawEnemy).ToArray(),
                    enemies.Count - 1);
            return enemyController;

        }

        private EnemyController AddToNextFreeSlot(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            if (enemies.Count >= slots.Count) return null;

            var instance = Instantiate(enemyControllerPrefab, slots[enemies.Count]);
            instance.Init(enemy, enemyModelInstance);
            Debug.Log($"Spawned enemy {enemyModelInstance.name}");
            enemies.Add(instance);

            return instance;
        }

        public void CheckDeadEnemies()
        {
            var changed = false;
            for (var i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].EnemyModelInstance.IsDead)
                {
                    changed = true;
                    Debug.Log($"Enemy ({enemies[i].EnemyModelInstance.name}) at {i} died");
                    Destroy(enemies[i].gameObject);
                    enemies.RemoveAt(i);
                }
            }

            if (changed)
            {
                Redraw();
                OnEnemyKilled?.Invoke();
            }

        }

        private void Redraw()
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                
                enemies[i].transform.SetParent(slots[i]);
                enemies[i].transform.DOLocalMove(Vector3.zero, 0.5f);
            }
        }

        public int Count()
        {
            return enemies.Count;
        }
    }
}