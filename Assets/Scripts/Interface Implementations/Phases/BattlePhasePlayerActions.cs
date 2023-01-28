using System;

public class BattlePhasePlayerActions : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public BattlePhasePlayerActions(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
        {
            logicQueue.AddElement(1.3f, () => { battleInstance.Player.deck.Cast(cardInstance, battleInstance.Player, battleInstance); });
            logicQueue.AddElement(0.1f, () => { battleInstance.Player.deck.DiscardCard(cardInstance); });
        }

        logicQueue.AddElement(0, () => OnFinish?.Invoke());
    }
}