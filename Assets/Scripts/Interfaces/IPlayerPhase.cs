using System;

public interface IPlayerPhase
{
    public event Action OnCancel;
    public event Action OnCompleted;
    void Start();
    void Terminate();
}