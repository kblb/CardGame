using System;

public class FightPhaseActorInstance
{
    private readonly IFightPhaseActor _fightPhaseActorModel;
    private float currentHealth;
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

    FightPhaseActorInstance(IFightPhaseActor fightPhaseActorModel)
    {
        _fightPhaseActorModel = fightPhaseActorModel;
        OnHealthChanged?.Invoke(Health);
    }
}