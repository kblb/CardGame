using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private FightPhaseScriptableObject fightPhaseScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;

    private AnimationQueue _animationQueue;

    private IGamePhase game;

    private void Start()
    {
        _animationQueue = new AnimationQueue(transform);

        FightPhaseActorInstance player = new FightPhaseActorInstance(playerScriptableObject);
        IEnumerable<FightPhaseActorInstance> enemies = fightPhaseScriptableObject.enemies.Select(t => new FightPhaseActorInstance(t));

        FightPhaseInstance fight = new FightPhaseInstance(fightPhaseScriptableObject);

        List<FightPhaseActorInstance> allActors = new(enemies)
        {
            player
        };

        FightPhaseSpawnOneEnemyInLastSlotIfEmpty fightPhaseSpawnOneEnemyInLastSlotIfEmpty = new FightPhaseSpawnOneEnemyInLastSlotIfEmpty(fight);

        fight.OnEnemySpawned += fightView.OnEnemySpawned;
        fightView.SpawnPlayer(player);

        FightPhasePlayerAction fightPhasePlayerAction = new FightPhasePlayerAction();
        fightView.uiView.cardCommitAreaView.OnCommitClicked += fightPhasePlayerAction.OnFinish;

        player.deck.NewCardDrawn += fightView.uiView.handView.AddCard;

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(
                new IFightPhase[]
                {
                    new FightPhasePullCardsFromHand(player.deck, 5),
                    new FightPhaseBuffsApply(allActors),
                    fightPhaseSpawnOneEnemyInLastSlotIfEmpty,
                    fightPhasePlayerAction,
                    new FightPhaseEnemyActions(),
                }
            )
        });

        game.Start();
    }
}