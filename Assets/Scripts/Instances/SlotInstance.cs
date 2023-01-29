using System;

public class SlotInstance
{
    public ActorInstance actor;
    public event Action OnActorChanged;

    public bool IsFree()
    {
        return actor == null;
    }

    public void PlaceActorHere(ActorInstance owner)
    {
        actor = owner;
        OnActorChanged?.Invoke();
    }
}