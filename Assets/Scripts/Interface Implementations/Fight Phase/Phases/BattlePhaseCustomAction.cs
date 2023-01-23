using System;

public class BattlePhaseCustomAction : IBattlePhase
{
    private readonly Action action;
    private readonly LogicQueue logicQueue;

    public BattlePhaseCustomAction(Action action, LogicQueue logicQueue)
    {
        this.action = action;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        logicQueue.AddElement(() => action?.Invoke());
        logicQueue.AddElement(() => OnFinish?.Invoke());
    }
}