using System;

public interface IGamePhase
{
    void Start();
    
    Action OnFinish { get; set; }
}