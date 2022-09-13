﻿using System.Collections.Generic;
using System.Linq;
using Cards;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyQueue : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private List<Transform> slots;
        [SerializeField] [AssetsOnly] private EnemyController enemyControllerPrefab;
        private List<EnemyController> enemies;

        private void Awake()
        {
            enemies = new List<EnemyController>();
        }

        public bool AddEnemy(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            return AddToNextFreeSlot(enemy, enemyModelInstance);
        }

        private bool AddToNextFreeSlot(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            if (enemies.Count >= slots.Count) return false;

            var instance = Instantiate(enemyControllerPrefab, slots[enemies.Count]);
            instance.Init(enemy, enemyModelInstance);
            enemies.Add(instance);

            return true;
        }

        public void AttackEnemies(List<Card> cards)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var enemy = enemies.First();
                if (!enemy.AttackEnemy(card)) continue;
                
                enemies.RemoveAt(0);
                Destroy(enemy.gameObject);
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

        public Attack AttackPlayer(PlayerModel playerModel)
        {
            var enemy = enemies.First();
            var attack = enemy.SelectedAttack;
            enemy.SelectNextAttack(playerModel, enemies.Select(e => e.RawEnemy).ToArray(), 0);
            return attack;
        }

        public void PrepareNextRound(PlayerModel playerModel)
        {
            enemies.First().SelectNextAttack(playerModel, enemies.Select(e => e.RawEnemy).ToArray(), 0);

            for (var i = 0; i < enemies.Count; i++)
            {
                var e = enemies[i];
                foreach (var buff in e.EnemyModelInstance.Buffs) buff.ApplyEffect(e);
                e.EnemyModelInstance.ClearBuffs();
            }

            for (var i = 0; i < enemies.Count; i++) enemies[i].ApplyPassiveToQueue(playerModel, enemies.ToArray(), i);
        }
    }
}