using System;

public class FightPhaseCustomAction : IFightPhase
{
    private readonly Action action;
    private readonly LogicQueue logicQueue;

    public FightPhaseCustomAction(Action action, LogicQueue logicQueue)
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