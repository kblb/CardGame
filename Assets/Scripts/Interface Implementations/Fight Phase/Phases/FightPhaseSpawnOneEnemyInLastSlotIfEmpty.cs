using System;

public class FightPhaseSpawnOneEnemyInLastSlotIfEmpty : IFightPhase
{
    private readonly FightPhaseInstance fight;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public FightPhaseSpawnOneEnemyInLastSlotIfEmpty(FightPhaseInstance fight, LogicQueue logicQueue)
    {
        this.fight = fight;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        SlotInstance lastSlot = fight.slots[^1];
        if (lastSlot.IsFree())
        {
            logicQueue.AddElement(() => { fight.SpawnEnemyAtSlotIndex(fight.slots.Count - 1); });
        }

        logicQueue.AddElement(() => { OnFinish?.Invoke(); });
    }
}