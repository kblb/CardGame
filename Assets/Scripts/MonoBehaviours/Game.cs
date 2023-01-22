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
        FightPhaseInstance fight = new(fightPhaseScriptableObject, playerScriptableObject);
        FightPhaseActorInstance player = fight.player;

        fight.OnEnemySpawned += (int slotIndex, SlotInstance slot) =>
        {
            fightView.SpawnEnemyAt(slotIndex, slot);
            slot.actor.deck.OnIntentUpdated += () => fightView.slotsView.UpdateIntent(slot);
        };
        player.deck.NewCardDrawn += (card) =>
        {
            CreateNewCardView(card, player);
            fightView.uiView.ShowHand(player.deck.hand);
        };

        player.deck.OnCardAddedToHand += instance =>
        {
            fightView.uiView.ShowHand(player.deck.hand);
            fightView.uiView.ShowCommitArea(player.deck.intent);
        };
        player.deck.OnIntentUpdated += () =>
        {
            fightView.uiView.ShowHand(player.deck.hand);
            fightView.uiView.ShowCommitArea(player.deck.intent);
            fightView.uiView.CommitButtonEnable(player.deck.intent.Count == 2);
        };

        FightPhasePlayerAction fightPhasePlayerAction = new();
        fightView.uiView.cardCommitAreaView.OnCommitClicked += fightPhasePlayerAction.InvokeFinish;

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(
                new IFightPhase[]
                {
                    new FightPhaseCustomAction(() => fightView.SpawnPlayer(player), logicQueue)
                }),
            new GamePhaseFight(
                new IFightPhase[]
                {
                    new FightPhasePullCardsFromHand(player.deck, 5, logicQueue),
                    new FightPhaseBuffsApply(fight.GetAllActors()),
                    new FightPhaseSpawnOneEnemyInLastSlotIfEmpty(fight, logicQueue),
                    new FightPhaseEnemiesDecideOnIntent(fight.slots),
                    fightPhasePlayerAction,
                    new FightPhaseEnemyActions(fight.enemies, player, logicQueue),
                }
            )
        });

        game.Start();
    }

    private void CreateNewCardView(CardInstance cardInstance, FightPhaseActorInstance player)
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
                && player.deck.intent.Count < 2)
            {
                player.deck.AddCardToCommitArea(cardInstance);
            }

            else if (fightView.uiView.cardCommitAreaView.isMouseHoveringOverMe.IsHovering == false
                     && player.deck.intent.Contains(cardInstance))
            {
                player.deck.RemoveCardFromCommitArea(cardInstance);
            }
            else
            {
                fightView.uiView.ShowHand(player.deck.hand);
                fightView.uiView.ShowCommitArea(player.deck.intent);
            }
        };
    }
}