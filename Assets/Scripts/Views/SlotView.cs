using DG.Tweening;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public ActorView actorView;
    
    public void MoveActorHere(ActorView actorViewArg)
    {
        this.actorView = actorViewArg;
        if (actorViewArg != null)
        {
            actorViewArg.transform.DOMove(transform.position, 0.5f);
            actorViewArg.transform.DOScale(transform.localScale, 0.5f);
        }
    }
}