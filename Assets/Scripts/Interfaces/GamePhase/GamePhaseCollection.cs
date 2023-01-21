using System;
using System.Linq;

public class GamePhaseCollection : IGamePhase
{
    private readonly IGamePhase[] _collection;

    public GamePhaseCollection(IGamePhase[] collection)
    {
        _collection = collection;

    }

    public void Start()
    {
        bool hasStarted = false;
        foreach (IGamePhase gamePhase in _collection)
        {
            if (gamePhase.IsFinished() == false)
            {
                gamePhase.Start();
                hasStarted = true;
                break;
            }
        }

        if (hasStarted == false)
        {
            throw new Exception("Game phase collection can not start. All elements are finished.");
        }
    }

    public bool IsFinished()
    {
        return _collection.All(t => t.IsFinished());
    }
}