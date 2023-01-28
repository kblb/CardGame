using System;
using System.Collections.Generic;

public class BattlePhaseEnemiesDecideOnIntent : IBattlePhase
{
    private readonly List<SlotInstance> fightSlots;
    private readonly LogicQueue logicQueue;
    private readonly CardInstance sleepCard;
    public Action OnFinish { get; set; }

    public BattlePhaseEnemiesDecideOnIntent(List<SlotInstance> fightSlots, LogicQueue logicQueue, CardInstance sleepCard)
    {
        this.fightSlots = fightSlots;
        this.logicQueue = logicQueue;
        this.sleepCard = sleepCard;
    }

    public void Start()
    {
        int i = 0;
        foreach (SlotInstance slotInstance in fightSlots)
        {
            ActorInstance enemy = slotInstance.actor;
            if (enemy != null)
            {
                int anotherLoopIndex = i;
                logicQueue.AddElement(0.5f, () =>
                {
                    CardInstance cardInstance = null;
                    if (anotherLoopIndex == 0)
                    {
                        if (enemy.deck.drawPile.Count == 0)
                        {
                            enemy.deck.ReshuffleDeck();
                        }

                        cardInstance = enemy.deck.DrawCard();
                    }
                    else
                    {
                        cardInstance = sleepCard;
                    }

                    enemy.deck.AddCardToCommitArea(cardInstance);

                    slotInstance.actor.deck.OnIntentReadyInvoke();
                });
            }

            i++;
        }

        logicQueue.AddElement(0, () => { OnFinish?.Invoke(); });
    }
}