using System;
using System.Collections.Generic;
using System.Linq;

public class BattlePhaseEnemiesDecideOnIntent : IBattlePhase
{
    private readonly List<SlotInstance> fightSlots;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public BattlePhaseEnemiesDecideOnIntent(List<SlotInstance> fightSlots, LogicQueue logicQueue)
    {
        this.fightSlots = fightSlots;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (SlotInstance slotInstance in fightSlots)
        {
            ActorInstance enemy = slotInstance.actor;
            if (enemy != null)
            {
                logicQueue.AddElement(0.5f, () =>
                {
                    if (enemy.deck.drawPile.Count == 0)
                    {
                        enemy.deck.ReshuffleDeck();
                    }

                    enemy.deck.AddCardToCommitArea(enemy.deck.DrawCard());
                    slotInstance.actor.deck.OnIntentReadyInvoke();
                });
            }
        }

        logicQueue.AddElement(0, () => { OnFinish?.Invoke(); });
    }
}