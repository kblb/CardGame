using System;
using System.Collections.Generic;

public class BattlePhaseEnemiesDecideOnIntent : IBattlePhase
{
    private readonly List<SlotInstance> fightSlots;
    private readonly LogicQueue logicQueue;
    private readonly ActorInstance target;
    public Action OnFinish { get; set; }

    public BattlePhaseEnemiesDecideOnIntent(List<SlotInstance> fightSlots, LogicQueue logicQueue, ActorInstance target)
    {
        this.fightSlots = fightSlots;
        this.logicQueue = logicQueue;
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
                    if (anotherLoopIndex == 0)
                    {
                        if (enemy.deck.drawPile.Count == 0)
                        {
                            enemy.deck.ReshuffleDeck();
                        }

                        AttackCardInstance cardInstance = enemy.deck.DrawCard() as AttackCardInstance;
                        IntentInstance intent = new IntentInstance(enemy, cardInstance, target);
                        enemy.deck.AddIntent(intent);
                    }
                });
            }

            i++;
        }

        logicQueue.AddElement(0, () => { OnFinish?.Invoke(); });
    }
}