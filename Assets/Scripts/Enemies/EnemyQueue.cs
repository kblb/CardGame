using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyQueue : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private List<Transform> slots;
        [SerializeField] [AssetsOnly] private EnemyController enemyControllerPrefab;
        private List<EnemyController> _enemies;
        public IEnumerable<EnemyController> Enemies => _enemies;

        private void Awake()
        {
            _enemies = new List<EnemyController>();
        }
        public event Action OnEnemyKilled;

        public EnemyController AddEnemy(
            EnemyModel enemy,
            EnemyModelInstance enemyModelInstance,
            PlayerModel playerModel)
        {
            var enemyController = AddToNextFreeSlot(enemy, enemyModelInstance);
            if (enemyController != null)
                enemyController.SelectNextAttack(playerModel, _enemies.Select(x => x.RawEnemy).ToArray(),
                    _enemies.Count - 1);
            return enemyController;

        }

        private EnemyController AddToNextFreeSlot(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            if (_enemies.Count >= slots.Count) return null;

            var instance = Instantiate(enemyControllerPrefab, slots[_enemies.Count]);
            instance.Init(enemy, enemyModelInstance);
            Debug.Log($"Spawned enemy {enemyModelInstance.name}");
            _enemies.Add(instance);

            return instance;
        }

        public void CheckDeadEnemies()
        {
            var changed = false;
            for (var i = _enemies.Count - 1; i >= 0; i--)
                if (_enemies[i].EnemyModelInstance.IsDead)
                {
                    changed = true;
                    Debug.Log($"Enemy ({_enemies[i].EnemyModelInstance.name}) at {i} died");
                    Destroy(_enemies[i].gameObject);
                    _enemies.RemoveAt(i);
                }

            if (!changed) return;

            Redraw();
            OnEnemyKilled?.Invoke();
        }

        private void Redraw()
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                
                _enemies[i].transform.SetParent(slots[i]);
                _enemies[i].transform.DOLocalMove(Vector3.zero, 0.5f);
            }
        }

        public void PrepareNextRound(PlayerModel playerModel)
        {
            _enemies.First().SelectNextAttack(playerModel, _enemies.Select(e => e.RawEnemy).ToArray(), 0);

            for (var i = 0; i < _enemies.Count; i++)
            {
                var e = _enemies[i];
                foreach (var buff in e.EnemyModelInstance.Buffs) buff.ApplyEffect(e);
                e.EnemyModelInstance.ClearBuffs();
            }

            for (var i = 0; i < _enemies.Count; i++)
                _enemies[i].ApplyPassiveToQueue(playerModel, _enemies.ToArray(), i);
        }

        public int Count()
        {
            return _enemies.Count;
        }
    }
}