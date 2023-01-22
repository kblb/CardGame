using System;
using System.Collections.Generic;
using System.Linq;

public class FightPhasePlayerActions : IFightPhase
{
    private readonly FightPhaseActorInstance player;
    private readonly List<FightPhaseActorInstance> fightEnemies;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public FightPhasePlayerActions(FightPhaseActorInstance player, List<FightPhaseActorInstance> fightEnemies, LogicQueue logicQueue)
    {
        this.player = player;
        this.fightEnemies = fightEnemies;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (CardInstance cardInstance in player.deck.intents)
        {
            logicQueue.AddElement(() => { cardInstance.CastOn(fightEnemies.First()); });
        }
    }
}