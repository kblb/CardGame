using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SlotsView : MonoBehaviour
{
    [SceneObjectsOnly, SerializeField] public SlotView playerSlot;
    [SceneObjectsOnly, SerializeField] public SlotView[] enemySlots;

    public ActorView SpawnEnemyAt(int slotIndex, FightPhaseActorInstance actor)
    {
        return enemySlots[slotIndex].SpawnActor(actor, actor.scriptableObject.prefab);
    }

    public ActorView FindActorView(FightPhaseActorInstance actor)
    {
        foreach (SlotView enemySlot in enemySlots)
        {
            if (enemySlot.actorView != null
                && enemySlot.actorView.actorInstance == actor)
            {
                return enemySlot.actorView;
            }
        }

        if (playerSlot.actorView.actorInstance == actor)
        {
            return playerSlot.actorView;
        }

        throw new Exception($"There are no actor view for actor instance {actor} ");
    }
}