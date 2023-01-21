using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private FightPhaseScriptableObject fightPhaseScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;

    private AnimationQueue _animationQueue ;

    private IGamePhase game;

    private void Start()
    {
        _animationQueue = new AnimationQueue(transform);

        FightPhaseActorInstance playerActor = new FightPhaseActorInstance(playerScriptableObject);
        IEnumerable<FightPhaseActorInstance> enemies = fightPhaseScriptableObject.enemies.Select(t => new FightPhaseActorInstance(t));
        
        FightPhaseInstance fight = new FightPhaseInstance(fightPhaseScriptableObject);

        List<FightPhaseActorInstance> allActors = new(enemies)
        {
            playerActor
        };

        FightPhaseSpawnOneEnemyInLastSlotIfEmpty fightPhaseSpawnOneEnemyInLastSlotIfEmpty = new FightPhaseSpawnOneEnemyInLastSlotIfEmpty(fight);
        
        fight.OnEnemySpawned += fightView.OnEnemySpawned;
        fightView.SpawnPlayer(playerActor);

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(
                new IFightPhase[]
                {
                    new FightPhaseBuffsApply(allActors),
                    fightPhaseSpawnOneEnemyInLastSlotIfEmpty,
                    new FightPhasePlayerAction(),
                    new FightPhaseEnemyActions(),
                }
            )
        });

        game.Start();
    }
}