using System;

public class FightPhaseReshuffleDeck : IFightPhase
{
    private readonly DeckInstance deck;

    public Action OnFinish { get; set; }
    public FightPhaseReshuffleDeck(DeckInstance deck)
    {
        this.deck = deck;
    }

    public void Start()
    {
        deck.ReshuffleDeck();
        OnFinish?.Invoke();
    }

}
