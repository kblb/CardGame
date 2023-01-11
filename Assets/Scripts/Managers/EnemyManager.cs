using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Cards.Effects;
using DG.Tweening;
using Enemies;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] public EnemyQueue enemyQueue;
        [SerializeField] [SceneObjectsOnly] private PlayerManager playerManager;

        private int enemyCreatedSinceStart;
        private AnimationQueue _animationQueue;

        public void Init(AnimationQueue animationQueue)
        {
            this._animationQueue = animationQueue;
        }

        [Button]
        public void SpawnEnemy(EnemyModel enemy)
        {
            var instance = new EnemyModelInstance(enemy, "Monster " + (++enemyCreatedSinceStart));
            enemyQueue.AddEnemy(enemy, instance, playerManager.PlayerModel);
        }

        public void AttackEnemies(List<Card> cards)
        {
            _animationQueue.AddElement(() =>
            {
                playerManager.playerController.view.ShowAttackAnimation();
            });

            foreach (Card card in cards)
            {
                if (card.effects != null)
                {
                    foreach (ICardEffect effect in card.effects)
                    {
                        _animationQueue.AddElement(() =>
                        {
                            effect.Apply(playerManager.PlayerModel, enemyQueue.Enemies.Select(e => e.EnemyModelInstance).ToArray());
                            enemyQueue.CheckDeadEnemies();
                        });
                    }
                }
            }
            _animationQueue.AddElement(() =>
            {
                playerManager.playerController.view.HideAttackAnimation();
            });
        }

        public void AttackPlayer(PlayerModel playerModel)
        {
            int i = 0;
            foreach (EnemyController enemy in enemyQueue.Enemies)
            {
                Attack attack = enemy.SelectedAttack;
                if (attack is { Damage: > 0f })
                {
                    _animationQueue.AddElement(() =>
                    {
                        if (enemy != null)
                        {
                            enemy.ShowAttackAnimation();
                        }
                    });

                    _animationQueue.AddElement(() =>
                    {
                        if (enemy != null)
                        {
                            enemy.SelectNextAttack(playerModel, enemyQueue.Enemies.Select(e => e.RawEnemy).ToArray(), i++);
                            Debug.Log($"Monster ({enemy.EnemyModelInstance.name}) attacking player with ({attack.GetType().Name} with {attack.Effect?.GetType().Name})");
                            playerManager.AttackPlayer(attack);
                        }
                    });
                    _animationQueue.AddElement(() =>
                    {
                        if (enemy != null)
                        {
                            enemy.HideAttackAnimation();
                        }
                    });
                }
            }
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