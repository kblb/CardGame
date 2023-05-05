using System;
using System.Linq;

public class AddNeighboursAttackModify : IModify
{
    public void Modify(CastInstance castInstance, BattleInstance battleInstance)
    {
        //getting left enemy of current target
        
        ActorInstance mostLeft = castInstance.targets.OrderBy(t => battleInstance.allEnemies.IndexOf(t)).First();
        int indexOfMostLeft = battleInstance.allEnemies.IndexOf(mostLeft);
        
        ActorInstance mostRight = castInstance.targets.OrderBy(t => battleInstance.allEnemies.IndexOf(t)).Last();
        int indexOfMostRight = battleInstance.allEnemies.IndexOf(mostRight);
        
        
        if (indexOfMostLeft > 0)
        {
            castInstance.targets.Insert(0, battleInstance.allEnemies[indexOfMostLeft -1]);
        }

        if (indexOfMostRight < battleInstance.allEnemies.Count - 1)
        {
            castInstance.targets.Add(battleInstance.allEnemies[indexOfMostRight + 1]);
        }
    }
}
