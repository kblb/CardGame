using System;

public class FightPhasePlayerAction : IFightPhase
{
    public Action OnFinish { get; set; }

    public FightPhasePlayerAction()
    {
        
    }

    public void Start()
    {
        //we're doing nothing here, waiting for the commit button
    }

}
