using System;
using System.Collections.Generic;

public class FightPhaseInstance
{
    public readonly FightPhaseScriptableObject scriptableObject;
    public List<FightPhaseActorInstance> enemies = new List<FightPhaseActorInstance>();
    public readonly List<SlotInstance> slots = new List<SlotInstance>();
    public int spawnedCount = 0;
    
    public Action<int, SlotInstance> OnEnemySpawned;

    public FightPhaseInstance(FightPhaseScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;

        for (int i = 0; i < scriptableObject.enemySlots; i++)
        {
            slots.Add(new SlotInstance());
        }
    }

    public void SpawnEnemyAtLastSlot()
    {
        FightPhaseActorInstance enemy = new FightPhaseActorInstance(scriptableObject.enemies[spawnedCount++]);
        enemies.Add(enemy);
        int slotIndex = slots.Count - 1;
        slots[slotIndex].actor = enemy;
        OnEnemySpawned?.Invoke(slotIndex, slots[slotIndex]);
    }
}
