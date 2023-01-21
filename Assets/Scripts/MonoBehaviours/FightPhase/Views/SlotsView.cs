using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class SlotsView : MonoBehaviour
{
    [SceneObjectsOnly, SerializeField] public SlotView playerSlot;
    [FormerlySerializedAs("slots")] [SceneObjectsOnly, SerializeField] public SlotView[] enemySlots;

    public void SpawnEnemyAt(int slotIndex, SlotInstance slotInstance)
    {
        enemySlots[slotIndex].SpawnActor(slotInstance.actor);
    }
}