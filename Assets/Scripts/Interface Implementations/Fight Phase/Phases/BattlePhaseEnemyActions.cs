using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhaseEnemyActions : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;

    public Action OnFinish { get; set; }

    public BattlePhaseEnemyActions(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (ActorInstance enemy in battleInstance.enemies)
        {
            foreach (CardInstance cardInstance in enemy.deck.intents)
            {
                logicQueue.AddElement(() =>
                {
                    ActionInstance actionInstance = cardInstance.CreateActionInstance(battleInstance.player);
                    actionInstance.Act();
                });
            }
        }

        logicQueue.AddElement(() => { OnFinish?.Invoke(); });
    }
}