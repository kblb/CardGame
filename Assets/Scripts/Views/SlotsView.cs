﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class SlotsView : MonoBehaviour
{
    [SceneObjectsOnly, SerializeField] public SlotView playerSlot;
    [SceneObjectsOnly, SerializeField] public SlotView[] enemySlots;
    [SerializeField, AssetsOnly] public ActorView actorViewPrefab;

    public List<ActorView> actorViews = new();
    private static int createdEnemies;

    public ActorView CreateNewActorView(ActorInstance actor, bool isPlayer)
    {
        ActorView actorView = Instantiate(actorViewPrefab, transform);
        actorView.Init(actor);
        if (isPlayer)
        {
            playerSlot.actorView = actorView;
            actorView.name = "Player";
        }
        else
        {
            actorView.name += ++createdEnemies;
        }

        actorViews.Add(actorView);

        return actorView;
    }

    public void ShowActors(List<SlotInstance> battleInstanceSlots, ActorInstance playerInstance)
    {
        ActorView playerView = actorViews.FirstOrDefault(t => t.actorInstance == playerInstance);
        if (playerView != null)
        {
            playerView.transform.DOMove(playerSlot.transform.position, 0.5f);
            playerView.transform.DOScale(playerSlot.transform.localScale, 0.5f);
        }

        int i = 0;
        foreach (SlotInstance slotInstance in battleInstanceSlots)
        {
            if (slotInstance.actor != null)
            {
                ActorView actorView = actorViews.FirstOrDefault(t => t.actorInstance == slotInstance.actor);
                enemySlots[i].MoveActorHere(actorView);
            }

            i++;
        }
    }

    public void DestroyActor(ActorInstance actorInstance)
    {
        ActorView actorView = actorViews.First(t => t.actorInstance == actorInstance);
        actorViews.Remove(actorView);
        Destroy(actorView.gameObject);
    }

    public ActorView FindActorView(ActorInstance target)
    {
        return actorViews.Find(t => t.actorInstance == target);
    }

    public void Highlight(List<ActorInstance> actors)
    {
        IEnumerable<ActorView> actorsToHighlight = actors.Select(FindActorView);

        foreach (ActorView actor in actorViews)
        {
            if (actorsToHighlight.Contains(actor))
            {
                actor.Highlight();
            }
            else
            {
                actor.TurnOffHighlight();
            }
        }
    }

    public void TurnOffHighlight(List<ActorInstance> battleInstanceAllEnemies)
    {
        foreach (ActorInstance actor in battleInstanceAllEnemies)
        {
            FindActorView(actor).TurnOffHighlight();
        }
    }

    public SlotView FindSlotView(ActorView actorView)
    {
        return playerSlot.actorView == actorView ? playerSlot : enemySlots.First(t => t.actorView == actorView);
    }
}