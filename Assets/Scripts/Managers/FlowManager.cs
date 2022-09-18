﻿using System.Collections.Generic;
using System.Linq;
using Cards;
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
        private void Awake()
        {
            cardsManager.AddOnCommitListener(CommitPlayerAttack);
        }

        private void CommitPlayerAttack(List<(int, Card)> cards)
        {
            enemyManager.AttackEnemies(cards.Select(e => e.Item2).ToList());
            enemyManager.AttackPlayer(playerModel);
            enemyManager.PrepareNextRound(playerModel);
        }
    }
}