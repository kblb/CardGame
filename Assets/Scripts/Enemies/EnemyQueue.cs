﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public event Action OnEnemyKilled;

        private void Awake()
        {
            _enemies = new List<EnemyController>();
        }

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
                    Destroy(_enemies[i].gameObject);
                    _enemies.RemoveAt(i);
                    Debug.Log($"Destroyed enemy at {i}");
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
                _enemies[i].transform.localPosition = Vector3.zero;
            }
        }

        public List<Attack> GetEnemyAttacks(PlayerModel playerModel)
        {
            return _enemies.Select((enemy, idx) =>
            {
                var attack = enemy.SelectedAttack;
                enemy.SelectNextAttack(playerModel, _enemies.Select(e => e.RawEnemy).ToArray(), idx);
                return attack;
            }).ToList();
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
        
        public int Count() => _enemies.Count;
    }
}