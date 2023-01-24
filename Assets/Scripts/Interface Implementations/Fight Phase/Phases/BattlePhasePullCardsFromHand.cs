using System;

public class BattlePhasePullCardsFromHand : IBattlePhase
{
    private readonly DeckInstance deck;
    private readonly int handSize;
    private readonly LogicQueue logicQueue;

    public BattlePhasePullCardsFromHand(DeckInstance deck, int handSize, LogicQueue logicQueue)
    {
        this.deck = deck;
        this.handSize = handSize;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        int count = handSize - deck.hand.Count;
        for (int i = 0; i < count; i++)
        {
            logicQueue.AddElement(() => { deck.DrawCard(); });
        }

        logicQueue.AddElement(() => { OnFinish?.Invoke(); });
    }
}