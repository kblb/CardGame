using System;

public class FightPhaseEnemiesShowIntent : IFightPhase
{
    public Action OnFinish { get; set; }
    public void Start()
    {
        throw new NotImplementedException();
    }
}
