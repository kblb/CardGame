using System;

public class PlayerPhaseSimultaneous : IPlayerPhase
{
    private readonly IPlayerPhase[] playerPhases;

    public event Action OnCancel;
    public event Action OnCompleted;
    
    public PlayerPhaseSimultaneous(IPlayerPhase[] playerPhases)
    {
        this.playerPhases = playerPhases;
    }

    public void Start()
    {
        foreach (IPlayerPhase playerPhase in playerPhases)
        {
            playerPhase.Start();
            playerPhase.OnCompleted += InvokeOnCompleted;
        }
    }

    private void InvokeOnCompleted()
    {
        OnCompleted?.Invoke();
    }

    public void Terminate()
    {
        foreach (IPlayerPhase playerPhase in playerPhases)
        {
            playerPhase.Terminate();
        }
    }
}
