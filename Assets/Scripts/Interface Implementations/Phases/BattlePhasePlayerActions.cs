using System;

public class BattlePhasePlayerActions : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;
    private readonly FightView fightView;
    public Action OnFinish { get; set; }

    public BattlePhasePlayerActions(BattleInstance battleInstance, LogicQueue logicQueue, FightView fightView)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
        this.fightView = fightView;
    }

    public void Start()
    {
        foreach (IntentInstance intent in battleInstance.Player.deck.intents)
        {
            logicQueue.AddElement(1.3f, () =>
            {
                intent.OnCast += fightView.OnCast;
                intent.Cast();
            });
        }

        logicQueue.AddElement(0, () => OnFinish?.Invoke());
    }
}