using System;
using Builders;
using UnityEngine;

public class BattlePhasePlayerCommit : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;

    public BattlePhasePlayerCommit(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }
    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Player Commit");
        var attack = new AttackBuilder(battleInstance, new Attack
        {
            damage = 2
        }, InitialTargetSelectionPolicy.Default);
        attack.AddTarget(0);

        foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
        {
            cardInstance.AppendToAttack(attack, battleInstance.Player, battleInstance);
        }

        Debug.Log(attack);
        AttackCollection attackCollection = attack.BuildAttack();
        attackCollection.Execute(battleInstance);

        // TODO: Animate building attack from cards

        foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
        {
            // logicQueue.AddElement(1.3f, () => { battleInstance.Player.deck.UseCard(cardInstance, battleInstance.Player, battleInstance); });
            logicQueue.AddElement(0.1f, () =>
            {
                battleInstance.Player.deck.DiscardCard(cardInstance);
            });
        }

        logicQueue.AddElement(0.5f, () => OnFinish?.Invoke());
    }
}