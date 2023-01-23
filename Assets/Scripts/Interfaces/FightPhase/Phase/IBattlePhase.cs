using System;

public interface IBattlePhase
{
    Action OnFinish { get; set; }
    void Start();
}