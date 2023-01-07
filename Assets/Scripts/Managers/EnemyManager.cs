using System;
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


        [Button]
        public void SpawnEnemy(EnemyModel enemy)
        {
            var instance = new EnemyModelInstance(enemy);
            enemyQueue.AddEnemy(enemy, instance, playerManager.PlayerModel);
        }

        public void AttackEnemies(List<Card> cards)
        {
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            foreach (var card in cards)
            {
                if (card.effects == null) continue;
                foreach (var effect in card.effects)
                {
                    sequence.Append(
                        transform.DOScale(Vector3.zero, 1)
                            .OnComplete(() =>
                            {
                                effect.Apply(
                                    playerManager.PlayerModel,
                                    enemyQueue.Enemies.Select(e => e.EnemyModelInstance).ToArray());
                                enemyQueue.CheckDeadEnemies();
                            })
                    );
                }
            }
            sequence.Play();
        }

        public void AttackPlayer(PlayerModel playerModel)
        {
            var attacks = enemyQueue.GetEnemyAttacks(playerModel);
            foreach (var attack in attacks)
                if (attack != null)
                    playerManager.AttackPlayer(attack);
        }

        public void PrepareNextRound(PlayerModel playerModel)
        {
            enemyQueue.PrepareNextRound(playerModel);
        }
        
        public int EnemyCount() => enemyQueue.Count();

        public void RegisterOnEnemyKilled(Action onEnemyKilled)
        {
            enemyQueue.OnEnemyKilled += onEnemyKilled;
        }
    }
}