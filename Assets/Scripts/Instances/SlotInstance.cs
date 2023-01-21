public class SlotInstance
{
    public FightPhaseActorInstance actor;

    public bool IsFree()
    {
        return actor == null;
    }
}
