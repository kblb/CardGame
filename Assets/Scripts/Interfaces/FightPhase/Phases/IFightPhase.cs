using System;

public interface IFightPhase
{
    Action OnFinish { get; set; }
    void Start();
}