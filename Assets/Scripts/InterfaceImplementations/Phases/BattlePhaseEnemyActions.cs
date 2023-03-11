using System;
using Builders;
using UnityEngine;

public class BattlePhaseEnemyActions : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;

    public BattlePhaseEnemyActions(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Enemy Actions");
        // FIXME: Sometimes some other async event modifies battleInstance and this crashes
        foreach (ActorInstance enemy in battleInstance.allEnemies)
        {
            var attack = new AttackBuilder(battleInstance, new Attack(), InitialTargetSelectionPolicy.Default);
            attack.AddTarget(AttackBuilder.PLAYER_TARGET_INDEX);

            foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
            {
                cardInstance.AppendToAttack(attack, enemy, battleInstance);
            }

            AttackCollection attackCollection = attack.BuildAttack();
            attackCollection.Execute(battleInstance);

            foreach (CardInstance cardInstance in enemy.deck.intents)
            {
                // logicQueue.AddElement(0.1f, () => { enemy.deck.UseCard(cardInstance, enemy, battleInstance); });
                logicQueue.AddElement(0.1f, () =>
                {
                    enemy.deck.DiscardCard(cardInstance);
                });
            }
        }

        logicQueue.AddElement(0, () =>
        {
            OnFinish?.Invoke();
        });
    }
}