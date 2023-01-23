using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SlotsView : MonoBehaviour
{
    [SceneObjectsOnly, SerializeField] public SlotView playerSlot;
    [SceneObjectsOnly, SerializeField] public SlotView[] enemySlots;

    public ActorView SpawnEnemyAt(int slotIndex, ActorInstance actor)
    {
        return enemySlots[slotIndex].SpawnActor(actor, actor.scriptableObject.prefab);
    }
}