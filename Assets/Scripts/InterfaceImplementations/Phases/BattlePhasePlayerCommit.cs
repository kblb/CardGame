using System;
using Builders;
using UnityEngine;

public class BattlePhasePlayerCommit : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;
    public Action OnFinish { get; set; }

    public BattlePhasePlayerCommit(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        var attack = new AttackBuilder(battleInstance);
        
        foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
        {
            cardInstance.AppendToAttack(attack, battleInstance.Player, battleInstance);
        }
        
        Debug.Log(attack);
        attack.Execute(battleInstance);
        
        // TODO: Animate building attack from cards
        
        foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
        {
            // logicQueue.AddElement(1.3f, () => { battleInstance.Player.deck.UseCard(cardInstance, battleInstance.Player, battleInstance); });
            logicQueue.AddElement(0.1f, () => { battleInstance.Player.deck.DiscardCard(cardInstance); });
        }

        logicQueue.AddElement(0, () => OnFinish?.Invoke());
    }
}