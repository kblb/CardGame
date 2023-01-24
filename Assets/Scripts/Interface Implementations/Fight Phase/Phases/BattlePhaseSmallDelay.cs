using System;

public class BattlePhaseSmallDelay : IBattlePhase
{
    private readonly float delay;
    private readonly LogicQueue logicQueue;

    public BattlePhaseSmallDelay(float delay, LogicQueue logicQueue)
    {
        this.delay = delay;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }
    public void Start()
    {
        logicQueue.AddElement(delay, () => { OnFinish?.Invoke();});
    }
}
