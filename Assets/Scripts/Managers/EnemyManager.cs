﻿using System.Collections.Generic;
using Cards;
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

        [Button]
        public void SpawnEnemy(EnemyModel enemy)
        {
            var instance = new EnemyModelInstance(enemy);
            enemyQueue.AddEnemy(enemy, instance, playerManager.PlayerModel);
        }

        public void AttackEnemies(List<Card> cards)
        {
            enemyQueue.AttackEnemies(cards);
        }

        public void AttackPlayer(PlayerModel playerModel)
        {
            var attack = enemyQueue.AttackPlayer(playerModel);
            if (attack != null) playerManager.AttackPlayer(attack);
        }

        public void PrepareNextRound(PlayerModel playerModel)
        {
            enemyQueue.PrepareNextRound(playerModel);
        }
    }
}