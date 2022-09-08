using System;
using System.Collections.Generic;
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

        private void CommitPlayerAttack(List<Card> cards)
        {
            enemyManager.AttackEnemies(cards);
            // TODO: After attack damage player, select next enemy attack and advance turn
            enemyManager.AttackPlayer(playerModel);
        }
    }
}