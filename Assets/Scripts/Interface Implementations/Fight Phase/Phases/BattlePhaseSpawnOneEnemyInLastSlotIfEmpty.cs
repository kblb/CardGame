using System;

public class BattlePhaseSpawnOneEnemyInLastSlotIfEmpty : IBattlePhase
{
    private readonly BattleInstance fight;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public BattlePhaseSpawnOneEnemyInLastSlotIfEmpty(BattleInstance fight, LogicQueue logicQueue)
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