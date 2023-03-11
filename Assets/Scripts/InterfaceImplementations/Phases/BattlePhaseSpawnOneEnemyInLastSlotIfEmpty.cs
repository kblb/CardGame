using System;
using UnityEngine;

public class BattlePhaseSpawnOneEnemyInLastSlotIfEmpty : IBattlePhase
{
    private readonly BattleInstance fight;
    private readonly LogicQueue logicQueue;

    public BattlePhaseSpawnOneEnemyInLastSlotIfEmpty(BattleInstance fight, LogicQueue logicQueue)
    {
        this.fight = fight;
        this.logicQueue = logicQueue;
    }
    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Spawn One Enemy In Last Slot If Empty");
        SlotInstance lastSlot = fight.slots[^1];
        if (fight.CanSpawnMoreEnemies() && lastSlot.IsFree())
        {
            logicQueue.AddElement(0.5f, () =>
            {
                fight.SpawnEnemyAtSlotIndex(fight.slots.Count - 1);
            });
        }

        logicQueue.AddElement(0, () =>
        {
            OnFinish?.Invoke();
        });
    }
}