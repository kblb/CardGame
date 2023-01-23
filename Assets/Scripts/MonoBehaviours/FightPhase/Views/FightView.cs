using UnityEngine;

public class FightView : MonoBehaviour
{
    [SerializeField] public SlotsView slotsView;
    [SerializeField] public UIView uiView;
    
    public ActorView SpawnEnemyAt(int slotIndex, ActorInstance actor)
    {
        return slotsView.SpawnEnemyAt(slotIndex, actor);
    }

    public void SpawnPlayer(ActorInstance playerActor)
    {
        slotsView.playerSlot.SpawnActor(playerActor, playerActor.scriptableObject.prefab);
    }
}
