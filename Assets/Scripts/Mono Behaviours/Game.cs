using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BattleScriptableObject battleScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;
    [SerializeField] private CardScriptableObject sleepCard;

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
            slot.OnActorChanged += () => { fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player); };
        }

        ActorInstance playerInstance = battleInstance.SpawnPlayer(playerScriptableObject);

        foreach (CardInstance cardInstance in battleInstance.Player.deck.drawPile)
        {
            fightView.uiView.CreateCardView(cardInstance);
        }

        fightView.uiView.ShowDrawPile(battleInstance.Player.deck.drawPile);

        playerInstance.deck.OnNewCardDrawn += (card) =>
        {
            fightView.uiView.ShowDrawPile(playerInstance.deck.drawPile);
            fightView.uiView.ShowHand(playerInstance.deck.hand);
        };

        playerInstance.deck.OnCardAddedToHand += instance =>
        {
            fightView.uiView.ShowHand(playerInstance.deck.hand);
        };
        playerInstance.deck.OnIntentUpdated += () =>
        {
            fightView.uiView.ShowHand(playerInstance.deck.hand);
        };
        playerInstance.deck.OnDrawPileReshuffled += () =>
        {
            fightView.uiView.ShowDrawPile(playerInstance.deck.drawPile);
            fightView.uiView.ShowDiscardPile(playerInstance.deck.discardPile);
        };

        fightView.OnCastFinished += (intent) =>
        {
            foreach (ActorInstance actor in battleInstance.GetAllActors())
            {
                if (actor.deck.intents.Contains(intent))
                {
                    actor.deck.DiscardIntent(intent);
                }
            }
        };

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(true,
                new IBattlePhase[]
                {
                    new BattlePhaseEnemiesMoveForward(battleInstance.slots, logicQueue),
                    new BattlePhaseApplyBuffs(battleInstance.GetAllActors(), logicQueue),
                    new BattlePhaseSpawnOneEnemyInLastSlotIfEmpty(battleInstance, logicQueue),
                    new BattlePhaseEnemiesDecideOnIntent(battleInstance.slots, logicQueue, new CardInstance(sleepCard), battleInstance.Player),
                    new BattlePhasePullCardsFromHand(battleInstance.Player.deck, 5, logicQueue),
                    new BattlePhaseWaitForInput(fightView, battleInstance),
                    new BattlePhasePlayerActions(battleInstance, logicQueue, fightView),
                    new BattlePhaseEnemyActions(battleInstance, logicQueue),
                }
            )
        });

        game.Start();
    }
}