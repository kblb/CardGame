using System.Collections.Generic;
using System.Linq;
using Cards;
using Enemies;
using Enemies.Passives.Effects;
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
            enemyManager.PlayerAct(cards.Select(e => e.Model).ToList());
            enemyManager.EnemiesAct(playerModel);

            enemyManager.enemyQueue.enemies.First().SelectNextAttack(playerModel, enemyManager.enemyQueue.enemies.Select(e => e.RawEnemy).ToArray(), 0);

            enemyManager.ApplyBuffs();

            enemyManager.EnemiesApplyPassives();
        }

        public void Init(AnimationQueue animationQueue)
        {
            _animationQueue = animationQueue;
        }
    }
}