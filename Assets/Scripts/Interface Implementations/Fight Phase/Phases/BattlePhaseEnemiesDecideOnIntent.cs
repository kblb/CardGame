using System;
using System.Collections.Generic;
using System.Linq;

public class BattlePhaseEnemiesDecideOnIntent : IBattlePhase
{
    private readonly List<SlotInstance> fightSlots;
    public Action OnFinish { get; set; }

    public BattlePhaseEnemiesDecideOnIntent(List<SlotInstance> fightSlots)
    {
        this.fightSlots = fightSlots;
    }

    public void Start()
    {
        foreach (SlotInstance slotInstance in fightSlots)
        {
            ActorInstance enemy = slotInstance.actor;
            if (enemy != null)
            {
                enemy.deck.AddCardToCommitArea(enemy.deck.DrawCard());
                slotInstance.actor.deck.OnIntentReadyInvoke();
            }
        }
    }
}
