using System;

public class BattlePhaseWaitForInput : IBattlePhase
{
    private IPlayerPhase playerPhases;

    public Action OnFinish { get; set; }

    public BattlePhaseWaitForInput(FightView fightView, BattleInstance battleInstance)
    {
        SelectCardAndMoveUpPlayerPhase selectCardAndMoveUpPlayerPhase = new SelectCardAndMoveUpPlayerPhase(fightView, battleInstance);
        selectCardAndMoveUpPlayerPhase.OnCompleted += () =>
        {
            battleInstance.Player.deck.AddIntent(new IntentInstance(
                battleInstance.Player,
                selectCardAndMoveUpPlayerPhase.selectedCard.cardInstance as AttackCardInstance, 
                selectCardAndMoveUpPlayerPhase.selectedTarget.actorInstance
            ));
        };

        ShowIntentSlotWithAdditionalSlotsPlayerPhase showIntentSlotWithAdditionalSlotsPlayerPhase = new ShowIntentSlotWithAdditionalSlotsPlayerPhase(fightView, battleInstance.Player.deck);

        playerPhases = new PlayerPhaseCollection(new IPlayerPhase[]
        {
            selectCardAndMoveUpPlayerPhase,
            showIntentSlotWithAdditionalSlotsPlayerPhase
        });

        playerPhases.OnCompleted += () => { OnFinish?.Invoke(); };
    }

    public void Start()
    {
        playerPhases.Start();
    }
}