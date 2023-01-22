using System;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    private ActorView actorView;
    
    public void SpawnActor(FightPhaseActorInstance actor, ActorView prefab)
    {
        if (this.actorView != null)
        {
            throw new Exception("Enemy already in slot. Will not spawn.");
        }

        this.actorView = Instantiate(prefab, transform);
        this.actorView.Init(actor);
    }
}