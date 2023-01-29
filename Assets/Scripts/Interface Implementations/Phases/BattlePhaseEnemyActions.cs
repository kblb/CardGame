using System;

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
        foreach (ActorInstance enemy in battleInstance.allEnemies)
        {
            foreach (CardInstance cardInstance in enemy.deck.intents)
            {
                logicQueue.AddElement(0.1f, () => { enemy.deck.Cast(cardInstance, enemy, battleInstance); });
                logicQueue.AddElement(0.1f, () => { enemy.deck.DiscardCard(cardInstance); });
            }
        }

        logicQueue.AddElement(0, () => { OnFinish?.Invoke(); });
    }
}