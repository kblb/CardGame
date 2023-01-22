using System;
using System.Collections.Generic;

internal class FightPhaseApplyBuffs : IFightPhase
{
    public IEnumerable<FightPhaseActorInstance> allActors;

    public Action OnFinish { get; set; }
    public FightPhaseApplyBuffs(IEnumerable<FightPhaseActorInstance> allActors)
    {
        this.allActors = allActors;
    }
    
    public void Start()
    {
        foreach (FightPhaseActorInstance phaseActor in allActors)
        {
            phaseActor.ApplyBuffs();
            phaseActor.ReduceBuffAmount();
        }
        OnFinish?.Invoke();
    }
}