using System;
using UnityEngine;

public class BattlePhaseWaitForInput : IBattlePhase
{
    private IPlayerPhase playerPhases;

    public Action OnFinish { get; set; }

    public BattlePhaseWaitForInput(FightView fightView, BattleInstance battleInstance)
    {
        SelectCardAndMoveUpPlayerPhase selectCardAndMoveUpPlayerPhase = new SelectCardAndMoveUpPlayerPhase(fightView, battleInstance);
        selectCardAndMoveUpPlayerPhase.OnCompleted += () =>
        {
            battleInstance.Player.inventory.deck.AddIntent(new IntentInstance(
                battleInstance.Player,
                selectCardAndMoveUpPlayerPhase.selectedCard.cardInstance,
                selectCardAndMoveUpPlayerPhase.selectedTarget.actorInstance
            ));
        };

        DragJewelsToCardsPlayerPhase dragJewelsToCardsPlayerPhase = new DragJewelsToCardsPlayerPhase(battleInstance, fightView);

        playerPhases = new PlayerPhaseSimultaneous(new IPlayerPhase[]
        {
            selectCardAndMoveUpPlayerPhase,
            dragJewelsToCardsPlayerPhase
        });

        playerPhases.OnCompleted += OnFinishInvoked;
    }

    public void OnFinishInvoked()
    {
        playerPhases.Terminate();
        OnFinish?.Invoke();
    }

    public void Start()
    {
        playerPhases.Start();
    }
}