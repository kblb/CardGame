using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private FightPhaseScriptableObject fightPhaseScriptableObject;
    [SerializeField] private List<CardScriptableObject> playersDeck;

    private AnimationQueue _animationQueue;

    private IGamePhase game;

    private void Start()
    {
        _animationQueue.Init();

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(
                new FightPhaseDeckActor(playersDeck),
                fightPhaseScriptableObject.enemies.Select(t => new FightPhaseDeckActor(t.deck)).ToArray<IFightPhaseActor>(),
                new IFightPhase[]
                {
                    new FightPhaseBuffsApply(),
                    new FightPhaseSpawnOneEnemyInLastSlotIfEmpty(),
                    new FightPhasePlayerAction(),
                    new FightPhaseEnemyActions(),
                }
            )
        });

        game.Start();
    }
}