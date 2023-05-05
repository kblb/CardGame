using System;

public class PlayerPhaseSimultaneous : IPlayerPhase
{
    private readonly IPlayerPhase[] playerPhases;

    public event Action OnCancel;
    public event Action OnCompleted;

    public PlayerPhaseSimultaneous(IPlayerPhase[] playerPhases)
    {
        this.playerPhases = playerPhases;

        foreach (IPlayerPhase playerPhase in playerPhases)
        {
            playerPhase.OnCompleted += InvokeOnCompleted;
        }
    }

    public void Start()
    {
        foreach (IPlayerPhase playerPhase in playerPhases)
        {
            playerPhase.Start();
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