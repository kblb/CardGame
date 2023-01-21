using System;

public interface IFightPhase
{
    void Start();
    Action OnFinish { get; set; }
}