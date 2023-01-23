using System;

public class BattlePhaseSmallDelay : IBattlePhase
{
    private readonly LogicQueue logicQueue;

    public BattlePhaseSmallDelay(LogicQueue logicQueue)
    {
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }
    public void Start()
    {
        logicQueue.AddElement(() => { });
        logicQueue.AddElement(() => { OnFinish?.Invoke();});
    }
}
