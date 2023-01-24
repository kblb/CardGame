using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BattleScriptableObject battleScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;

    private readonly LogicQueue logicQueue = new();

    private IGamePhase game;

    private void Start()
    {
        BattleInstance battleInstance = new(battleScriptableObject);

        battleInstance.OnActorSpawned += (ActorInstance actorInstance) =>
        {
            actorInstance.deck.OnCardDiscarded += card => { fightView.uiView.ShowDiscardPile(actorInstance.deck.discardPile); };
            actorInstance.OnDeath += () =>
            {
                fightView.slotsView.DestroyActor(actorInstance);
                battleInstance.DestroyActor(actorInstance);
            };

            ActorView actorView = fightView.slotsView.CreateNewActorView(actorInstance);
            actorInstance.OnHealthChanged += () => actorView.statsView.SetHealth(actorInstance.scriptableObject.health, actorInstance.currentHealth);
            actorInstance.deck.OnIntentUpdated += () => actorView.UpdateIntent(actorInstance.deck.intents);
            actorInstance.deck.OnCardDiscarded += (card) => actorView.UpdateIntent(actorInstance.deck.intents);
            fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player);
        };

        foreach (SlotInstance slot in battleInstance.slots)
        {
            slot.OnActorMovedHere += () => { fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player); };
        }

        ActorInstance playerInstance = battleInstance.SpawnPlayer(playerScriptableObject);

        foreach (CardInstance cardInstance in battleInstance.Player.deck.drawPile)
        {
            CreateNewCardView(cardInstance, battleInstance.Player);
        }

        fightView.uiView.ShowDrawPile(battleInstance.Player.deck.drawPile);

        BattlePhasePlayerAction battlePhasePlayerAction = new();
        fightView.uiView.cardCommitAreaView.OnCommitClicked += battlePhasePlayerAction.InvokeFinish;


        playerInstance.deck.OnNewCardDrawn += (card) =>
        {
            fightView.uiView.ShowDrawPile(playerInstance.deck.drawPile);
            fightView.uiView.ShowHand(playerInstance.deck.hand);
        };

        playerInstance.deck.OnCardAddedToHand += instance =>
        {
            fightView.uiView.ShowHand(playerInstance.deck.hand);
            fightView.uiView.ShowCommitArea(playerInstance.deck.intents);
        };
        playerInstance.deck.OnIntentUpdated += () =>
        {
            fightView.uiView.ShowHand(playerInstance.deck.hand);
            fightView.uiView.ShowCommitArea(playerInstance.deck.intents);
        };
        playerInstance.deck.OnDrawPileReshuffled += () =>
        {
            fightView.uiView.ShowDrawPile(playerInstance.deck.drawPile);
            fightView.uiView.ShowDiscardPile(playerInstance.deck.discardPile);
        };

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(true,
                new IBattlePhase[]
                {
                    new BattlePhaseApplyBuffs(battleInstance.GetAllActors(), logicQueue),
                    new BattlePhaseSpawnOneEnemyInLastSlotIfEmpty(battleInstance, logicQueue),
                    new BattlePhaseEnemiesDecideOnIntent(battleInstance.slots, logicQueue),
                    new BattlePhasePullCardsFromHand(battleInstance.Player.deck, 5, logicQueue),
                    battlePhasePlayerAction,
                    new BattlePhasePlayerActions(battleInstance, logicQueue),
                    new BattlePhaseEnemyActions(battleInstance, logicQueue),
                }
            )
        });

        logicQueue.AddElement(1f, () => { game.Start(); });
    }

    private void CreateNewCardView(CardInstance cardInstance, ActorInstance player)
    {
        CardView cardView = fightView.uiView.CreateCardView(cardInstance);
        cardView.draggableImage.OnDragNotification += () =>
        {
            fightView.uiView.cardCommitAreaView.Highlight(fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering);
        };
        cardView.draggableImage.OnExitDragNotification += () =>
        {
            if (fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering
                && player.deck.hand.Contains(cardInstance)
                && player.deck.intents.Count < 2)
            {
                player.deck.AddCardToCommitArea(cardInstance);
            }

            else if (fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering == false
                     && player.deck.intents.Contains(cardInstance))
            {
                player.deck.RemoveCardFromCommitArea(cardInstance);
            }
            else
            {
                fightView.uiView.ShowHand(player.deck.hand);
                fightView.uiView.ShowCommitArea(player.deck.intents);
            }
        };
    }
}