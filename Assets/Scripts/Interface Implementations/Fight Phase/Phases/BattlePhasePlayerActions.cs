using System;
using System.Collections.Generic;
using System.Linq;

public class BattlePhasePlayerActions : IBattlePhase
{
    private readonly ActorInstance player;
    private readonly List<ActorInstance> fightEnemies;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public BattlePhasePlayerActions(ActorInstance player, List<ActorInstance> fightEnemies, LogicQueue logicQueue)
    {
        this.player = player;
        this.fightEnemies = fightEnemies;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (CardInstance cardInstance in player.deck.intents)
        {
            logicQueue.AddElement(() =>
            {
                ActionInstance actionInstance = cardInstance.CreateActionInstance(fightEnemies.First());
                actionInstance.Act();
            });
        }

        logicQueue.AddElement(() => { OnFinish?.Invoke(); });
    }
}