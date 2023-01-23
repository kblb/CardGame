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
        BattleInstance battleInstance = new(battleScriptableObject, playerScriptableObject);
        ActorInstance player = battleInstance.player;

        battleInstance.OnEnemySpawned += (int slotIndex, ActorInstance enemy) =>
        {
            ActorView actorView = fightView.SpawnEnemyAt(slotIndex, enemy);

            enemy.deck.OnIntentUpdated += () => actorView.UpdateIntent(enemy.deck.intents);
            enemy.OnHealthChanged += () => { actorView.statsView.SetHealth(enemy.scriptableObject.health, enemy.currentHealth); };
        };

        player.deck.NewCardDrawn += (card) =>
        {
            CreateNewCardView(card, player);
            fightView.uiView.ShowHand(player.deck.hand);
        };

        player.deck.OnCardAddedToHand += instance =>
        {
            fightView.uiView.ShowHand(player.deck.hand);
            fightView.uiView.ShowCommitArea(player.deck.intents);
        };
        player.deck.OnIntentUpdated += () =>
        {
            fightView.uiView.ShowHand(player.deck.hand);
            fightView.uiView.ShowCommitArea(player.deck.intents);
            fightView.uiView.CommitButtonEnable(player.deck.intents.Count == 2);
        };

        player.OnHealthChanged += () => fightView.slotsView.playerSlot.actorView.statsView.SetHealth(player.scriptableObject.health, player.currentHealth);
        player.deck.OnIntentUpdated += () => fightView.slotsView.playerSlot.actorView.UpdateIntent(player.deck.intents);

        BattlePhasePlayerAction battlePhasePlayerAction = new();
        fightView.uiView.cardCommitAreaView.OnCommitClicked += battlePhasePlayerAction.InvokeFinish;

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(false,
                new IBattlePhase[]
                {
                    new BattlePhaseCustomAction(() => fightView.SpawnPlayer(player), logicQueue)
                }),
            new GamePhaseFight(true,
                new IBattlePhase[]
                {
                    new BattlePhasePullCardsFromHand(player.deck, 5, logicQueue),
                    new BattlePhaseApplyBuffs(battleInstance.GetAllActors()),
                    new BattlePhaseSpawnOneEnemyInLastSlotIfEmpty(battleInstance, logicQueue),
                    new BattlePhaseEnemiesDecideOnIntent(battleInstance.slots),
                    battlePhasePlayerAction,
                    new BattlePhasePlayerActions(player, battleInstance.enemies, logicQueue),
                    new BattlePhaseEnemyActions(battleInstance, logicQueue),
                }
            )
        });

        game.Start();
    }

    private void CreateNewCardView(CardInstance cardInstance, ActorInstance player)
    {
        CardView cardView = fightView.uiView.CreateCardView(cardInstance);
        cardView.draggableImage.OnDragNotification += () =>
        {
            fightView.uiView.cardCommitAreaView.Highlight(fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering);
            fightView.uiView.handView.Highlight(fightView.uiView.handView.isMouseHoveringOverMe.IsHovering);
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