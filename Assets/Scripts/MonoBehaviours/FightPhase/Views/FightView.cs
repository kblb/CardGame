using UnityEngine;

public class FightView : MonoBehaviour
{
    [SerializeField] public SlotsView slotsView;
    [SerializeField] public UIView uiView;
    
    public void OnEnemySpawned(int slotIndex, SlotInstance slotInstance)
    {
        slotsView.SpawnEnemyAt(slotIndex, slotInstance);
    }

    public void SpawnPlayer(FightPhaseActorInstance playerActor)
    {
        slotsView.playerSlot.SpawnActor(playerActor, playerActor.scriptableObject.prefab);
    }
}
