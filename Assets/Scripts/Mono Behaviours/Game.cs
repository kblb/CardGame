using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private BattleScriptableObject battleScriptableObject;
    [SerializeField] private ActorScriptableObject playerScriptableObject;
    [SerializeField] private FightView fightView;

    [SerializeField] private CardScriptableObject sleepAttackCard;

    private readonly LogicQueue logicQueue = new();

    private IGamePhase game;

    private void Start()
    {
        BattleInstance battleInstance = new BattleInstance(battleScriptableObject);

        fightView.OnCasted += (intent) => intent.Cast(battleInstance);

        fightView.uiView.jewelsFrame.OnJewelFinishedAnimatingToSlot += itemView => { battleInstance.Player.inventory.AddJewel(itemView.instance); };

        battleInstance.OnActorDestroyed += instance => { fightView.slotsView.DestroyActor(instance); };

        battleInstance.OnActorSpawned += (ActorInstance actorInstance, bool isPlayer) =>
        {
            actorInstance.inventory.deck.OnCardDiscarded += card => { fightView.uiView.ShowDiscardPile(actorInstance.inventory.deck.discardPile); };
            actorInstance.OnZeroHealth += () =>
            {
                SlotView slotView = fightView.slotsView.enemySlots.First(t => (t.actorView != null ? t.actorView.actorInstance : null) == actorInstance);

                if (actorInstance.scriptableObject.lootGenerator != null)
                {
                    fightView.uiView.jewelsFrame.SpawnItemViewAndAnimateToSlot(
                        0,
                        actorInstance.scriptableObject.lootGenerator.Generate(),
                        Camera.main.WorldToScreenPoint(slotView.actorView.transform.position));
                }

                battleInstance.DestroyActor(actorInstance);
            };

            ActorView actorView = fightView.slotsView.CreateNewActorView(actorInstance, isPlayer);
            actorInstance.OnHealthChanged += () => actorView.statsView.SetHealth(actorInstance.scriptableObject.health, actorInstance.currentHealth);
            actorInstance.OnShieldsChanged += () => actorView.statsView.SetShields(actorInstance.currentShields);
            actorInstance.inventory.deck.OnIntentUpdated += () => { actorView.UpdateIntent(actorInstance.inventory.deck.intent); };
            actorInstance.inventory.deck.OnCardDiscarded += (card) => actorView.UpdateIntent(actorInstance.inventory.deck.intent);
            fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player);
        };

        foreach (SlotInstance slot in battleInstance.slots)
        {
            slot.OnActorChanged += () => { fightView.slotsView.ShowActors(battleInstance.slots, battleInstance.Player); };
        }

        ActorInstance playerInstance = battleInstance.SpawnPlayer(playerScriptableObject);
        
        playerInstance.inventory.OnJewelRemoved += instance =>
        {
            JewelView jewelView = fightView.uiView.FindJewelView(instance);
            Destroy(jewelView.gameObject);
        };


        foreach (CardInstance cardInstance in battleInstance.Player.inventory.deck.drawPile)
        {
            CardView cardView = fightView.uiView.CreateCardView(cardInstance);
            cardInstance.OnJewelAdded += cardView.AnimateNewJewel;
        }

        fightView.uiView.ShowDrawPile(battleInstance.Player.inventory.deck.drawPile);

        playerInstance.inventory.deck.OnCardAddedToHand += (card) => { fightView.uiView.ShowHand(playerInstance.inventory.deck.hand); };
        playerInstance.inventory.deck.OnCardRemovedFromHand += () => { fightView.uiView.ShowHand(playerInstance.inventory.deck.hand); };
        playerInstance.inventory.deck.OnCardRemovedFromDrawPile += (card) => { fightView.uiView.ShowDrawPile(playerInstance.inventory.deck.hand); };
        playerInstance.inventory.deck.OnDrawPileReshuffled += () =>
        {
            fightView.uiView.ShowDrawPile(playerInstance.inventory.deck.drawPile);
            fightView.uiView.ShowDiscardPile(playerInstance.inventory.deck.discardPile);
        };

        fightView.OnCastFinished += (intent) =>
        {
            foreach (ActorInstance actor in battleInstance.GetAllActors())
            {
                if (actor.inventory.deck.intent == intent)
                {
                    actor.inventory.deck.DiscardIntent(intent);
                }
            }
        };

        BattlePhaseWaitForInput battlePhaseWaitForInput = new BattlePhaseWaitForInput(fightView, battleInstance);

        fightView.uiView.endTurn.onClick.AddListener(() => { battlePhaseWaitForInput.OnFinishInvoked(); });

        game = new GamePhaseCollection(new IGamePhase[]
        {
            new GamePhaseFight(true,
                new IBattlePhase[]
                {
                    new BattlePhaseCancelAllShields(battleInstance, logicQueue),
                    new BattlePhaseEnemiesMoveForward(battleInstance.slots, logicQueue),
                    new BattlePhaseApplyBuffs(battleInstance.GetAllActors(), logicQueue),
                    new BattlePhaseSpawnOneEnemyInLastSlotIfEmpty(battleInstance, logicQueue),
                    new BattlePhaseEnemiesDecideOnIntent(battleInstance.slots, logicQueue, new CardInstance(sleepAttackCard), battleInstance.Player),
                    new BattlePhasePullCardsFromHand(battleInstance.Player.inventory.deck, 2, logicQueue),
                    battlePhaseWaitForInput,
                    new BattlePhasePlayerActions(battleInstance, logicQueue, fightView),
                    new BattlePhaseEnemyActions(battleInstance, logicQueue),
                }
            )
        });

        game.Start();
    }
}