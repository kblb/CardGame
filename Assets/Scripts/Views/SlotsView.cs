using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SlotsView : MonoBehaviour
{
    [SceneObjectsOnly, SerializeField] public SlotView playerSlot;
    [SceneObjectsOnly, SerializeField] public SlotView[] enemySlots;
    [SerializeField, AssetsOnly] public ActorView actorViewPrefab;

    private List<ActorView> actorViews = new();

    public ActorView CreateNewActorView(ActorInstance actor)
    {
        ActorView actorView = Instantiate(actorViewPrefab, transform);
        actorView.Init(actor);

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
                if (actorView != null)
                {
                    actorView.transform.DOMove(enemySlots[i].transform.position, 0.5f);
                    actorView.transform.DOScale(enemySlots[i].transform.localScale, 0.5f);
                }
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
}