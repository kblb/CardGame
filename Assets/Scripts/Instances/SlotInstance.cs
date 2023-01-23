public class SlotInstance
{
    public ActorInstance actor;

    public bool IsFree()
    {
        return actor == null;
    }
}
