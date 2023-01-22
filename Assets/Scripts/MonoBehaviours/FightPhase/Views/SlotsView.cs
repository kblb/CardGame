using Sirenix.OdinInspector;
using UnityEngine;

public class SlotsView : MonoBehaviour
{
    [SceneObjectsOnly, SerializeField] public SlotView playerSlot;
    [SceneObjectsOnly, SerializeField] public SlotView[] enemySlots;

    public void SpawnEnemyAt(int slotIndex, SlotInstance slotInstance)
    {
        enemySlots[slotIndex].SpawnActor(slotInstance.actor, slotInstance.actor.scriptableObject.prefab);
    }
}