using System;
using System.Collections.Generic;

public class FightPhaseInstance
{
    public readonly FightPhaseScriptableObject scriptableObject;
    public readonly List<FightPhaseActorInstance> enemies = new();
    public readonly FightPhaseActorInstance player;
    public readonly List<SlotInstance> slots = new();

    public Action<int, FightPhaseActorInstance> OnEnemySpawned;

    public FightPhaseInstance(FightPhaseScriptableObject scriptableObject, ActorScriptableObject playerScriptableObject)
    {
        this.scriptableObject = scriptableObject;

        for (int i = 0; i < scriptableObject.enemySlots; i++)
        {
            slots.Add(new SlotInstance());
        }

        player = new FightPhaseActorInstance(playerScriptableObject);
    }

    public void SpawnEnemyAtSlotIndex(int index)
    {
        FightPhaseActorInstance enemy = new(scriptableObject.enemies[enemies.Count]);
        enemies.Add(enemy);
        if (slots[index].IsFree() == false)
        {
            throw new Exception($"Will not spawn enemy at slot {index}, because it is already occupied");
        }

        slots[index].actor = enemy;
        OnEnemySpawned?.Invoke(index, enemy);
    }

    public List<FightPhaseActorInstance> GetAllActors()
    {
        return new List<FightPhaseActorInstance>(enemies)
        {
            player
        };
    }
}