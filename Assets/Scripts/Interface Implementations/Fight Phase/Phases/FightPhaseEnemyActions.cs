using System;
using System.Collections.Generic;

public class FightPhaseEnemyActions : IFightPhase
{
    private readonly IEnumerable<FightPhaseActorInstance> enemies;
    private readonly FightPhaseActorInstance player;
    private readonly LogicQueue logicQueue;

    public FightPhaseEnemyActions(IEnumerable<FightPhaseActorInstance> enemies, FightPhaseActorInstance player, LogicQueue logicQueue)
    {
        this.enemies = enemies;
        this.player = player;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }
    public void Start()
    {
        foreach (FightPhaseActorInstance enemy in enemies)
        {
        }
    }

}