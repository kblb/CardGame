using System;
using System.Collections.Generic;

internal class BattlePhaseApplyBuffs : IBattlePhase
{
    public IEnumerable<ActorInstance> allActors;
    private readonly LogicQueue logicQueue;

    public Action OnFinish { get; set; }

    public BattlePhaseApplyBuffs(IEnumerable<ActorInstance> allActors, LogicQueue logicQueue)
    {
        this.allActors = allActors;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (ActorInstance phaseActor in allActors)
        {
            logicQueue.AddElement(0.5f, () =>
            {
                phaseActor.ApplyBuffs();
                phaseActor.ReduceBuffAmount();
            });
        }

        OnFinish?.Invoke();
    }
}