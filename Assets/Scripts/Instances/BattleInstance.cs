using System;
using System.Collections.Generic;

public class BattleInstance
{
    public readonly BattleScriptableObject scriptableObject;
    public readonly List<ActorInstance> enemies = new();
    public readonly ActorInstance player;
    public readonly List<SlotInstance> slots = new();

    public Action<int, ActorInstance> OnEnemySpawned;

    public BattleInstance(BattleScriptableObject scriptableObject, ActorScriptableObject playerScriptableObject)
    {
        this.scriptableObject = scriptableObject;

        for (int i = 0; i < scriptableObject.enemySlots; i++)
        {
            slots.Add(new SlotInstance());
        }

        player = new ActorInstance(playerScriptableObject);
    }

    public void SpawnEnemyAtSlotIndex(int index)
    {
        ActorInstance enemy = new(scriptableObject.enemies[enemies.Count]);
        enemies.Add(enemy);
        if (slots[index].IsFree() == false)
        {
            throw new Exception($"Will not spawn enemy at slot {index}, because it is already occupied");
        }

        slots[index].actor = enemy;
        OnEnemySpawned?.Invoke(index, enemy);
    }

    public List<ActorInstance> GetAllActors()
    {
        return new List<ActorInstance>(enemies)
        {
            player
        };
    }
}