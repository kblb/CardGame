using System;

public class FightPhasePlayerAction : IFightPhase
{
    public Action OnFinish { get; set; }

    public void Start()
    {
        throw new System.NotImplementedException();
    }

}
