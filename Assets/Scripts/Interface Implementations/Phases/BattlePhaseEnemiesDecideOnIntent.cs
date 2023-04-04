using System;
using System.Collections.Generic;

public class BattlePhaseEnemiesDecideOnIntent : IBattlePhase
{
    private readonly List<SlotInstance> fightSlots;
    private readonly LogicQueue logicQueue;
    private readonly CardInstance sleepAttackCard;
    private readonly ActorInstance target;
    public Action OnFinish { get; set; }

    public BattlePhaseEnemiesDecideOnIntent(List<SlotInstance> fightSlots, LogicQueue logicQueue, CardInstance sleepAttackCard, ActorInstance target)
    {
        this.fightSlots = fightSlots;
        this.logicQueue = logicQueue;
        this.sleepAttackCard = sleepAttackCard;
        this.target = target;
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
                logicQueue.AddElement(0.1f, () =>
                {
                    CardInstance cardInstance = null;
                    if (anotherLoopIndex == 0)
                    {
                        if (enemy.inventory.deck.drawPile.Count == 0)
                        {
                            enemy.inventory.deck.ReshuffleDeck();
                        }

                        cardInstance = enemy.inventory.deck.DrawCard();
                    }
                    else
                    {
                        cardInstance = sleepAttackCard;
                    }

                    IntentInstance intent = new IntentInstance(enemy, cardInstance, target);
                    enemy.inventory.deck.AddIntent(intent);
                });
            }

            i++;
        }

        logicQueue.AddElement(0, () => { OnFinish?.Invoke(); });
    }
}