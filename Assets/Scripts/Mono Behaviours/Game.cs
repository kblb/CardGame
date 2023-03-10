using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private BattleScriptableObject battleScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;

    [FormerlySerializedAs("sleepCard")] [SerializeField]
    private AttackCardScriptableObject sleepAttackCard;

    private readonly LogicQueue logicQueue = new();

    private IGamePhase game;

    private void Start()
    {

        BattleInstance battleInstance = new(battleScriptableObject);
        
        fightView.OnCasted += (intent) => intent.Cast(battleInstance);

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
            actorInstance.deck.OnIntentUpdated += () => actorView.UpdateIntent(actorInstance.deck.intent);
            actorInstance.deck.OnCardDiscarded += (card) => actorView.UpdateIntent(actorInstance.deck.intent);
            fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player);
        };

        foreach (SlotInstance slot in battleInstance.slots)
        {
            slot.OnActorChanged += () => { fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player); };
        }

        ActorInstance playerInstance = battleInstance.SpawnPlayer(playerScriptableObject);

        fightView.uiView.intentView.OnShown += () => { fightView.uiView.ShowIntent(playerInstance.deck.intent); };

        foreach (CardInstance cardInstance in battleInstance.Player.deck.drawPile)
        {
            fightView.uiView.CreateCardView(cardInstance);
        }

        fightView.uiView.ShowDrawPile(battleInstance.Player.deck.drawPile);

        playerInstance.deck.OnCardAddedToHand += (card) => { fightView.uiView.ShowHand(playerInstance.deck.hand); };
        playerInstance.deck.OnCardRemovedFromHand += () => { fightView.uiView.ShowHand(playerInstance.deck.hand); };
        playerInstance.deck.OnIntentUpdated += () => { fightView.uiView.ShowIntent(playerInstance.deck.intent); };
        playerInstance.deck.OnCardRemovedFromDrawPile += (card) => { fightView.uiView.ShowDrawPile(playerInstance.deck.hand); };
        playerInstance.deck.OnDrawPileReshuffled += () =>
        {
            fightView.uiView.ShowDrawPile(playerInstance.deck.drawPile);
            fightView.uiView.ShowDiscardPile(playerInstance.deck.discardPile);
        };

        fightView.OnCastFinished += (intent) =>
        {
            foreach (ActorInstance actor in battleInstance.GetAllActors())
            {
                if (actor.deck.intent == intent)
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
                    new BattlePhaseEnemiesDecideOnIntent(battleInstance.slots, logicQueue, new AttackCardInstance(sleepAttackCard), battleInstance.Player),
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