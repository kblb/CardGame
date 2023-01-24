using System;
using System.Linq;

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
            logicQueue.AddElement(() =>
            {
                battleInstance.Player.deck.Cast(cardInstance, battleInstance.Player, battleInstance);
            });
        }

        logicQueue.AddElement(() => { OnFinish?.Invoke(); });
    }
}