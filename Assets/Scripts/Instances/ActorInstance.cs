using System;
using System.Collections.Generic;

public class ActorInstance
{
    public readonly ActorScriptableObject scriptableObject;
    public float currentHealth;
    public List<BuffInstance> buffs = new();
    public DeckInstance deck;

    public event Action OnHealthChanged;
    public event Action OnDeath;

    public ActorInstance(ActorScriptableObject scriptableObject)
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

    public void ReceiveDamage(int amount)
    {
        if (amount < 1)
        {
            throw new Exception("Amount should be bigger than 0");
        }
        currentHealth -= amount;
        OnHealthChanged?.Invoke();
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}