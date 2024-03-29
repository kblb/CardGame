﻿public class IncreaseAttackModify : IModify
{
    public readonly int damageToAdd;

    public IncreaseAttackModify(int damageToAdd)
    {
        this.damageToAdd = damageToAdd;
    }
    
    public void Modify(CastInstance castInstance, BattleInstance battleInstance)
    {
        castInstance.damage += damageToAdd;
    }
}
