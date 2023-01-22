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

    public void SpawnEnemyAtSlotIndex(int index)
    {
        FightPhaseActorInstance enemy = new(scriptableObject.enemies[spawnedCount++]);
        enemies.Add(enemy);
        if (slots[index].IsFree() == false)
        {
            throw new Exception($"Will not spawn enemy at slot {index}, because it is already occupied");
        }
        slots[index].actor = enemy;
        OnEnemySpawned?.Invoke(index, slots[index]);
    }
}
