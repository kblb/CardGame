using UnityEngine;

public class FightView : MonoBehaviour
{
    [SerializeField] private SlotsView slotsView;
    
    public void OnEnemySpawned(int slotIndex, SlotInstance slotInstance)
    {
        slotsView.SpawnEnemyAt(slotIndex, slotInstance);
    }

    public void SpawnPlayer(FightPhaseActorInstance playerActor)
    {
        slotsView.playerSlot.SpawnActor(playerActor);
    }
}
