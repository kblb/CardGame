using System;
using DG.Tweening;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    [SerializeField] private ActorView actorViewPrefab;
    private ActorView actorView;
    
    public void SpawnActor(FightPhaseActorInstance actor)
    {
        if (this.actorView != null)
        {
            throw new Exception("Enemy already in slot. Will not spawn.");
        }

        this.actorView = Instantiate(actorViewPrefab, transform);
        this.actorView.Init(actor);
    }
}