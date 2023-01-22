using System;

public class FightPhasePullCardsFromHand : IFightPhase
{
    private readonly DeckInstance deck;
    private readonly int handSize;

    public FightPhasePullCardsFromHand(DeckInstance deck, int handSize)
    {
        this.deck = deck;
        this.handSize = handSize;
    }

    public Action OnFinish { get; set; }
    
    public void Start()
    {
        for (int i = deck.hand.Count; i < handSize; i++)
        {
            deck.DrawCard();
        }
        OnFinish?.Invoke();
    }

}
