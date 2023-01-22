using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private FightPhaseScriptableObject fightPhaseScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;

    private readonly LogicQueue logicQueue = new();

    private IGamePhase game;

    private void Start()
    {
        //initialization
        FightPhasePlayerAction fightPhasePlayerAction = new();
        FightPhaseActorInstance player = new(playerScriptableObject);
        IEnumerable<FightPhaseActorInstance> enemies = fightPhaseScriptableObject.enemies.Select(t => new FightPhaseActorInstance(t));
        FightPhaseInstance fight = new(fightPhaseScriptableObject);
        List<FightPhaseActorInstance> allActors = new(enemies)
        {
            player
        };

        //hooking events to view
        fight.OnEnemySpawned += fightView.OnEnemySpawned;
        player.deck.NewCardDrawn += (CardInstance cardInstance) =>
        {
            CardView cardView = fightView.uiView.CreateCardView(cardInstance);
            cardView.draggableImage.OnDragNotification += () =>
            {
                fightView.uiView.cardCommitAreaView.Highlight(fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering);
                fightView.uiView.handView.Highlight(fightView.uiView.handView.isMouseHoveringOverMe.IsHovering);
            };
            cardView.draggableImage.OnExitDragNotification += () =>
            {
                if (fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering)
                {
                    player.deck.AddCardToCommitArea(cardInstance);
                }
                else
                {
                    player.deck.AddCardToHand(cardInstance);
                }
            };
            fightView.uiView.ShowHand(player.deck.hand);
        };
        player.deck.OnCardAddedToCommitArea += (CardInstance CardInstance) =>
        {
            fightView.uiView.ShowHand(player.deck.hand);
            fightView.uiView.ShowCommitArea(player.deck.commitArea);
        };
        player.deck.OnCardAddedToHand += instance =>
        {
            fightView.uiView.ShowHand(player.deck.hand);
            fightView.uiView.ShowCommitArea(player.deck.commitArea);
        };
        
        //hooking view to events
        fightView.uiView.cardCommitAreaView.OnCommitClicked += fightPhasePlayerAction.OnFinish;
        //fightView.uiView.OnCardDraggedIntoCommitArea += 

        //configuration
        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(
                new IFightPhase[]
                {
                    new FightPhaseCustomAction(() =>  fightView.SpawnPlayer(player), logicQueue)
                }),
            new GamePhaseFight(
                new IFightPhase[]
                {
                    new FightPhasePullCardsFromHand(player.deck, 5, logicQueue),
                    new FightPhaseBuffsApply(allActors),
                    new FightPhaseSpawnOneEnemyInLastSlotIfEmpty(fight, logicQueue),
                    fightPhasePlayerAction,
                    new FightPhaseEnemyActions(),
                }
            )
        });

        game.Start();
    }
}