using System;
using System.Collections.Generic;

internal class BattlePhaseApplyBuffs : IBattlePhase
{
    public IEnumerable<ActorInstance> allActors;

    public Action OnFinish { get; set; }
    public BattlePhaseApplyBuffs(IEnumerable<ActorInstance> allActors)
    {
        this.allActors = allActors;
    }
    
    public void Start()
    {
        foreach (ActorInstance phaseActor in allActors)
        {
            phaseActor.ApplyBuffs();
            phaseActor.ReduceBuffAmount();
        }
        OnFinish?.Invoke();
    }
}