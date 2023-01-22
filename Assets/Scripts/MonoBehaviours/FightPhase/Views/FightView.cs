using UnityEngine;

public class FightView : MonoBehaviour
{
    [SerializeField] public SlotsView slotsView;
    [SerializeField] public UIView uiView;
    
    public ActorView SpawnEnemyAt(int slotIndex, FightPhaseActorInstance actor)
    {
        return slotsView.SpawnEnemyAt(slotIndex, actor);
    }

    public void SpawnPlayer(FightPhaseActorInstance playerActor)
    {
        slotsView.playerSlot.SpawnActor(playerActor, playerActor.scriptableObject.prefab);
    }
}
