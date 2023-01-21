using System;
using System.Linq;

public class GamePhaseFight : IGamePhase
{
    private readonly IFightPhase[] _fightPhases;
    private readonly IFightPhaseActor _player;
    private readonly IFightPhaseActor[] _enemies;

    public GamePhaseFight(IFightPhaseActor player, IFightPhaseActor[] enemies, IFightPhase[] fightPhases)
    {
        _fightPhases = fightPhases;
        _player = player;
        _enemies = enemies;

    }
    public void Start()
    {
        bool hasStarted = false;
        foreach (IFightPhase gamePhase in _fightPhases)
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
        return _fightPhases.All(t => t.IsFinished());
    }
}
