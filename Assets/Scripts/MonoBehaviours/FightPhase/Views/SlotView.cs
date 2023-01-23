using System;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public ActorView actorView;

    public ActorView SpawnActor(ActorInstance actor, ActorView prefab)
    {
        if (this.actorView != null)
        {
            throw new Exception("Enemy already in slot. Will not spawn.");
        }

        this.actorView = Instantiate(prefab, transform);
        this.actorView.Init(actor);
        return actorView;
    }
}