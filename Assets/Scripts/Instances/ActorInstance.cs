using System;
using System.Collections.Generic;
using System.Linq;
using Builders;
using UnityEngine;

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

    public bool ReceiveDamage(int amount, Affinity attackAffinity)
    {
        if (amount > 0) // Don't apply affinity to healing
        {
            float multiplierOnAttacking = attackAffinity.MultiplierOnAttacking(scriptableObject.affinity);
            Debug.Log($"Applying affinity {attackAffinity} vs {scriptableObject.affinity} to damage; multiplier: {multiplierOnAttacking}");

            amount = Mathf.RoundToInt(amount * multiplierOnAttacking);
        }
        currentHealth -= amount;
        OnHealthChanged?.Invoke();
        return currentHealth <= 0;
    }

    public bool ReceiveDamage(Attack attack) => 
        ReceiveDamage(attack.damage, attack.affinity);

    public void Die()
    {
        OnDeath?.Invoke();
    }
}