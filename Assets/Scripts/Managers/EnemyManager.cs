﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using DG.Tweening;
using Enemies;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private EnemyQueue enemyQueue;
        [SerializeField] [SceneObjectsOnly] private PlayerManager playerManager;

        private int enemyCreatedSinceStart;

        [Button]
        public void SpawnEnemy(EnemyModel enemy)
        {
            var instance = new EnemyModelInstance(enemy, "Monster " + (++enemyCreatedSinceStart));
            enemyQueue.AddEnemy(enemy, instance, playerManager.PlayerModel);
        }

        public void AttackEnemies(List<Card> cards)
        {
            foreach (var card in cards)
            {
                if (card.effects == null) continue;
                foreach (var effect in card.effects)
                {
                    // TODO: Revert delaying this with DOTween
                    effect.Apply(
                        playerManager.PlayerModel,
                        enemyQueue.Enemies.Select(e => e.EnemyModelInstance).ToArray());
                }
            }
            enemyQueue.CheckDeadEnemies();
        }

        public void AttackPlayer(PlayerModel playerModel)
        {
            int i = 0;
            foreach (EnemyController enemy in enemyQueue.Enemies)
            {
                Attack attack = enemy.SelectedAttack;
                if (attack != null)
                {
                    enemy.SelectNextAttack(playerModel, enemyQueue.Enemies.Select(e => e.RawEnemy).ToArray(), i++);
                    Debug.Log($"Monster ({enemy.EnemyModelInstance.name}) attacking player with ({attack.GetType().Name} with {attack.Effect?.GetType().Name})");
                    playerManager.AttackPlayer(attack);
                }
            }
        }

        public void PrepareNextRound(PlayerModel playerModel)
        {
            enemyQueue.PrepareNextRound(playerModel);
        }

        public int EnemyCount()
        {
            return enemyQueue.Count();
        }

        public void RegisterOnEnemyKilled(Action onEnemyKilled)
        {
            enemyQueue.OnEnemyKilled += onEnemyKilled;
        }
    }
}