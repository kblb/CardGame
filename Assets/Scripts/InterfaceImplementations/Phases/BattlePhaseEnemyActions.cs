using System;
using Builders;

public class BattlePhaseEnemyActions : IBattlePhase
{
    private readonly BattleInstance battleInstance;
    private readonly LogicQueue logicQueue;

    public Action OnFinish { get; set; }

    public BattlePhaseEnemyActions(BattleInstance battleInstance, LogicQueue logicQueue)
    {
        this.battleInstance = battleInstance;
        this.logicQueue = logicQueue;
    }

    public void Start()
    {
        foreach (ActorInstance enemy in battleInstance.allEnemies)
        {
            var attack = new AttackBuilder(battleInstance, new Attack(), InitialTargetSelectionPolicy.Default);
            attack.AddTarget(AttackBuilder.PLAYER_TARGET_INDEX);
            
            foreach (CardInstance cardInstance in battleInstance.Player.deck.intents)
            {
                cardInstance.AppendToAttack(attack, enemy, battleInstance);
            }

            var attackCollection = attack.BuildAttack();
            attackCollection.Execute(battleInstance);
            
            foreach (CardInstance cardInstance in enemy.deck.intents)
            {
                // logicQueue.AddElement(0.1f, () => { enemy.deck.UseCard(cardInstance, enemy, battleInstance); });
                logicQueue.AddElement(0.1f, () => { enemy.deck.DiscardCard(cardInstance); });
            }
        }

        logicQueue.AddElement(0, () => { OnFinish?.Invoke(); });
    }
}