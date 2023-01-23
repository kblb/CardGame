using System;

public class BattlePhasePlayerAction : IBattlePhase
{
    public Action OnFinish { get; set; }

    public void Start()
    {
        //we're doing nothing here, waiting for the commit button
    }

    public void InvokeFinish()
    {
        OnFinish?.Invoke();
    }
}
