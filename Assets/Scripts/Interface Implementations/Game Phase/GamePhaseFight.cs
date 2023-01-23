using System;
using System.Linq;

public class GamePhaseFight : IGamePhase
{
    private readonly bool loop;
    private readonly IBattlePhase[] _fightPhases;
    public Action OnFinish { get; set; }

    public GamePhaseFight(bool loop, IBattlePhase[] fightPhases)
    {
        this.loop = loop;
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
        if (loop)
        {
            Start();
        }
        else
        {
            OnFinish?.Invoke();
        }
    }

    public void Start()
    {
        _fightPhases.First().Start();
    }
}