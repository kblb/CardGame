using System;

public class BattlePhaseEnemiesShowIntent : IBattlePhase
{
    public Action OnFinish { get; set; }
    public void Start()
    {
        throw new NotImplementedException();
    }
}
