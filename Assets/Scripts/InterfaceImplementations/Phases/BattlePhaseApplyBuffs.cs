using System;
using System.Collections.Generic;
using UnityEngine;

internal class BattlePhaseApplyBuffs : IBattlePhase
{
    private readonly LogicQueue logicQueue;
    public IEnumerable<ActorInstance> allActors;

    public BattlePhaseApplyBuffs(IEnumerable<ActorInstance> allActors, LogicQueue logicQueue)
    {
        this.allActors = allActors;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Apply Buffs");
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