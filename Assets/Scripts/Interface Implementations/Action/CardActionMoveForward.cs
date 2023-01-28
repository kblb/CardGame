using System;

public class CardActionMoveForward : ICardAction
{
    public void Cast(ActorInstance owner, BattleInstance battleInstance)
    {
        battleInstance.MoveForward(owner);
    }
}