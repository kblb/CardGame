using System;
using System.Collections.Generic;
using System.Linq;
using Builders;

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
        buffs = this.scriptableObject.initialBuffs.Select(t => new BuffInstance(t, 999)).ToList();
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

    public void ReceiveDamage(int amount, Affinity attackAffinity)
    {
        foreach (BuffInstance buff in buffs)
        {
            amount = buff.AlterDamageReceived(amount, attackAffinity);
        }
        
        currentHealth -= amount;
        OnHealthChanged?.Invoke();
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}