using System;
using System.Collections.Generic;

public class FightPhaseActorInstance
{
    public readonly ActorScriptableObject scriptableObject;
    private float currentHealth;
    public List<BuffInstance> buffs = new();
    public List<ActionIntentInstance> intents = new();

    public event Action<float> OnHealthChanged;

    public float Health
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    public FightPhaseActorInstance(ActorScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
        OnHealthChanged?.Invoke(Health);
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