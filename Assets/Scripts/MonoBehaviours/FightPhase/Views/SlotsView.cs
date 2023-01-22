using System.Linq;
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

    public void UpdateIntent(SlotInstance slot)
    {
        enemySlots
            .First(t => t.actorView != null && t.actorView.actorInstance == slot.actor)
            .actorView.statsView.SetIntent(slot.actor.deck.intent);
    }
}