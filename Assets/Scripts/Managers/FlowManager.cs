using System.Collections.Generic;
using System.Linq;
using Cards;
using Enemies;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class FlowManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private CardsManager cardsManager;
        [SerializeField] [SceneObjectsOnly] private EnemyManager enemyManager;

        [SerializeField] private PlayerModel playerModel;
        private AnimationQueue _animationQueue;


        private void Awake()
        {
            cardsManager.AddOnCommitListener(CommitPlayerAttack);
        }

        private void CommitPlayerAttack(List<CardModelWrapper> cards)
        {
            enemyManager.AttackEnemies(cards.Select(e => e.Model).ToList());
            enemyManager.AttackPlayer(playerModel);
            
            enemyManager.enemyQueue.enemies.First().SelectNextAttack(playerModel, enemyManager.enemyQueue.enemies.Select(e => e.RawEnemy).ToArray(), 0);

            foreach (var e in enemyManager.enemyQueue.enemies)
            {
                foreach (var buff in e.EnemyModelInstance.Buffs)
                {
                    buff.ApplyEffect(e);
                }
                e.EnemyModelInstance.ClearBuffs();
            }

            for (var i = 0; i < enemyManager.enemyQueue.enemies.Count; i++)
            {
                int index = i;
                EnemyController currentEnemy = enemyManager.enemyQueue.enemies[index];
                if (currentEnemy.CanApplyPassive())
                {
                    _animationQueue.AddElement(() =>
                    {
                        currentEnemy.ShowAttackAnimation();
                    });
                    _animationQueue.AddElement(() =>
                    {
                        currentEnemy.ApplyPassiveToQueue(playerModel, enemyManager.enemyQueue.enemies.ToArray());
                        currentEnemy.HideAttackAnimation();
                    });
                }
            }
        }

        public void Init(AnimationQueue animationQueue)
        {
            _animationQueue = animationQueue;
        }
    }
}