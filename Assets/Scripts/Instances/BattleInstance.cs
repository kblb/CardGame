using System;
using System.Collections.Generic;
using System.Linq;

public class BattleInstance
{
    public readonly BattleScriptableObject scriptableObject;
    public readonly List<ActorInstance> allEnemies = new();
    private ActorInstance player;
    public readonly List<SlotInstance> slots = new();

    public Action<ActorInstance> OnActorSpawned;
    public ActorInstance Player => player;

    public BattleInstance(BattleScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;

        for (int i = 0; i < scriptableObject.enemySlots; i++)
        {
            slots.Add(new SlotInstance());
        }
    }

    public ActorInstance SpawnPlayer(ActorScriptableObject playerScriptableObject)
    {
        player = new(playerScriptableObject);
        OnActorSpawned?.Invoke(player);
        return player;
    }

    public void SpawnEnemyAtSlotIndex(int index)
    {
        ActorInstance enemy = new(scriptableObject.enemies[allEnemies.Count]);
        allEnemies.Add(enemy);
        if (slots[index].IsFree() == false)
        {
            throw new Exception($"Will not spawn enemy at slot {index}, because it is already occupied");
        }

        slots[index].actor = enemy;
        OnActorSpawned?.Invoke(enemy);
    }

    public List<ActorInstance> GetAllActors()
    {
        return new List<ActorInstance>(allEnemies)
        {
            player
        };
    }

    public bool CanMoveForward(ActorInstance actor)
    {
        return actor != player && slots.First().actor != actor;
    }

    public void MoveForward(ActorInstance owner)
    {
        
        SlotInstance currentSlot = slots.First(t => t.actor == owner);
        int currentSlotIndex = slots.IndexOf(currentSlot);

        currentSlot.PlaceActorHere(null);
        slots[currentSlotIndex - 1].PlaceActorHere(owner);
    }
}