using System;
using System.Linq;

public class GamePhaseCollection : IGamePhase
{
    private readonly IGamePhase[] gamePhases;
    public Action OnFinish { get; set; }

    public GamePhaseCollection(IGamePhase[] gamePhases)
    {
        this.gamePhases = gamePhases;
        for (int i = 0; i < gamePhases.Length; i++)
        {
            if (i < gamePhases.Length - 1)
            {
                gamePhases[i].OnFinish += gamePhases[i + 1].Start;
            }

            if (i == gamePhases.Length - 1)
            {
                gamePhases[i].OnFinish += InvokeFinish;
            }
        }
    }

    private void InvokeFinish()
    {
        OnFinish?.Invoke();
    }

    public void Start()
    {
        gamePhases.First().Start();
    }
}