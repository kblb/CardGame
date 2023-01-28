using System;

public class BattlePhasePlayerAction : IBattlePhase
{
    public Action OnFinish { get; set; }
    public Action<bool> OnCommitReady;

    public void Start()
    {
        //we're doing nothing here, waiting for the commit button
        OnCommitReady?.Invoke(true);
    }

    public void InvokeFinish()
    {
        OnCommitReady?.Invoke(false);
        OnFinish?.Invoke();
    }
}
