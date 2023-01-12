using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Cards.Effects;
using DG.Tweening;
using Enemies;
using Enemies.Passives.Effects;
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

        public void PlayerAct(List<Card> cards)
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

        public void EnemiesAct(PlayerModel playerModel)
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

        public void ApplyBuffs()
        {
            foreach (EnemyController e in enemyQueue.enemies)
            {
                EnemyController enemy = e;
                if (enemy.EnemyModelInstance.Buffs.Any())
                {
                    _animationQueue.AddElement(() =>
                    {
                        enemy.ShowBuffApplyAnimation();
                        foreach (IEnemyPassiveEffect buff in enemy.EnemyModelInstance.Buffs)
                        {
                            buff.ApplyEffect(e);
                        }
                    });
                    _animationQueue.AddElement(() =>
                    {
                        if (enemy != null)
                        {
                            enemy.HideBuffApplyAnimation();
                            e.EnemyModelInstance.ClearBuffs();
                        }
                    });
                }
            }
        }

        public void EnemiesApplyPassives()
        {
            for (var i = 0; i < enemyQueue.enemies.Count; i++)
            {
                int index = i;
                EnemyController currentEnemy = enemyQueue.enemies[index];
                if (currentEnemy.CanApplyPassive())
                {
                    _animationQueue.AddElement(() =>
                    {
                        currentEnemy.ShowAttackAnimation();
                    });
                    _animationQueue.AddElement(() =>
                    {
                        currentEnemy.ApplyPassiveToQueue(playerManager.PlayerModel, enemyQueue.enemies.ToArray());
                        currentEnemy.HideAttackAnimation();
                    });
                }
            }
        }
    }
}