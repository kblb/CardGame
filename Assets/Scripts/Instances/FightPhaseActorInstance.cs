using System;
using System.Collections.Generic;

public class FightPhaseActorInstance
{
    public readonly ActorScriptableObject scriptableObject;
    public float currentHealth;
    public List<BuffInstance> buffs = new();
    public DeckInstance deck;

    public FightPhaseActorInstance(ActorScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
        deck = new DeckInstance(scriptableObject.deck);
        currentHealth = scriptableObject.health;
    }

    public void ApplyBuffs()
    {
        foreach (BuffInstance buff in buffs)
        {
            throw new NotImplementedException();
        }
    }

    public void ReduceBuffAmount()
    {
        foreach (BuffInstance buff in buffs)
        {
            buff.amount--;
        }

        buffs.RemoveAll(t => t.amount <= 0);
    }
}