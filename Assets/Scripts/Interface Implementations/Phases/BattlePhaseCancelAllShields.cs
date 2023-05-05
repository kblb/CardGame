using System;
using UnityEngine;

public class BattlePhaseCancelAllShields : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;

    public Action OnFinish { get; set; }

    public BattlePhaseCancelAllShields(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        if (battleInstance.Player.currentShields > 0)
        {
            logicQueue.AddElement(0.5f, () => { battleInstance.Player.ResetShields(); });
        }

        foreach (ActorInstance enemy in battleInstance.allEnemies)
        {
            if (enemy.currentShields > 0)
            {
                logicQueue.AddElement(0.5f, () => { enemy.ResetShields(); });
            }
        }

        logicQueue.AddElement(0, () => OnFinish?.Invoke());
    }
}