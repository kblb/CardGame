using System;

public class FightPhaseEnemyActions : IFightPhase
{
    public Action OnFinish { get; set; }
    public void Start()
    {
        throw new System.NotImplementedException();
    }

}