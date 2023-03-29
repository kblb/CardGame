using System;
using System.Collections.Generic;
using System.Linq;

public class BattleInstance
{
    public readonly BattleScriptableObject scriptableObject;
    public readonly List<ActorInstance> allEnemies = new();
    private ActorInstance player;
    public readonly List<SlotInstance> slots = new();
    private int enemiesCreated;

    public Action<ActorInstance, bool> OnActorSpawned;
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
        player = new ActorInstance(playerScriptableObject);
        OnActorSpawned?.Invoke(player, true);
        return player;
    }

    public void SpawnEnemyAtSlotIndex(int index)
    {
        if (enemiesCreated >= scriptableObject.enemies.Count)
        {
            throw new Exception("We shouldn't spawn next enemy, because we reached already the end of level.");
        }
        ActorInstance enemy = new ActorInstance(scriptableObject.enemies[enemiesCreated++]);
        SpawnAtSlotIndex(index, enemy); 
    }

    public void SpawnAtSlotIndex(int index, ActorInstance enemy)
    {
        if (slots[index].IsFree() == false)
        {
            throw new Exception($"Will not spawn enemy at slot {index}, because it is already occupied");
        }
        allEnemies.Add(enemy);
        slots[index].actor = enemy;
        OnActorSpawned?.Invoke(enemy, false);
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

    public void DestroyActor(ActorInstance actorInstance)
    {
        allEnemies.Remove(actorInstance);
        SlotInstance slotInstance = slots.First(t => t.actor == actorInstance);
        slotInstance.actor = null;
    }

    public bool CanSpawnMoreEnemies()
    {
        return enemiesCreated < scriptableObject.enemies.Count;
    }
}