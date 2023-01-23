using System;

public class SlotInstance
{
    public ActorInstance actor;
    public event Action OnActorMovedHere;

    public bool IsFree()
    {
        return actor == null;
    }

    public void PlaceActorHere(ActorInstance owner)
    {
        actor = owner;
        if (owner != null)
        {
            OnActorMovedHere?.Invoke();
        }
    }
}