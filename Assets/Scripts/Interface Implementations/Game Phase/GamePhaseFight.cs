using System;
using System.Linq;

public class GamePhaseFight : IGamePhase
{
    private readonly IFightPhase[] _fightPhases;
    public Action OnFinish { get; set; }

    public GamePhaseFight(IFightPhase[] fightPhases)
    {
        _fightPhases = fightPhases;

        for (int i = 0; i < fightPhases.Length; i++)
        {
            if (i < fightPhases.Length - 1)
            {
                fightPhases[i].OnFinish += fightPhases[i + 1].Start;
            }

            if (i == fightPhases.Length - 1)
            {
                fightPhases[i].OnFinish += InvokeFinish;
            }
        }
    }

    private void InvokeFinish()
    {
        OnFinish?.Invoke();
    }

    public void Start()
    {
        _fightPhases.First().Start();
    }

}
