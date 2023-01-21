using System;

public class FightPhaseSpawnOneEnemyInLastSlotIfEmpty : IFightPhase
{
    private readonly FightPhaseInstance fight;
    public Action OnFinish { get; set; }

    public FightPhaseSpawnOneEnemyInLastSlotIfEmpty(FightPhaseInstance fight)
    {
        this.fight = fight;
    }

    public void Start()
    {
        SlotInstance lastSlot = fight.slots[^1];
        if (lastSlot.IsFree())
        {
            fight.SpawnEnemyAtLastSlot();
        }
        OnFinish?.Invoke();
    }
}
