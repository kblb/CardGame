using System.Collections.Generic;

public class FightPhaseDeckActor : IFightPhaseActor
{
    private readonly List<CardScriptableObject> _deck;

    public FightPhaseDeckActor(List<CardScriptableObject> deck)
    {
        _deck = deck;
    }
}