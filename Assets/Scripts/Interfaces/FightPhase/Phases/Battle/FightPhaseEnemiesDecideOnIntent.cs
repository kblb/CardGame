using System;
using System.Collections.Generic;
using System.Linq;

public class FightPhaseEnemiesDecideOnIntent : IFightPhase
{
    private readonly List<SlotInstance> fightSlots;
    public Action OnFinish { get; set; }

    public FightPhaseEnemiesDecideOnIntent(List<SlotInstance> fightSlots)
    {
        this.fightSlots = fightSlots;
    }

    public void Start()
    {
        foreach (SlotInstance slotInstance in fightSlots)
        {
            FightPhaseActorInstance enemy = slotInstance.actor;
            if (enemy != null)
            {
                enemy.deck.AddCardToCommitArea(enemy.deck.DrawCard());
                slotInstance.actor.deck.OnIntentReadyInvoke();
            }
        }
    }
}
