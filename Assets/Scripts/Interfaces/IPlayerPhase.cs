using System;

public interface IPlayerPhase
{
    public event Action OnCompleted;
    void Start();
}