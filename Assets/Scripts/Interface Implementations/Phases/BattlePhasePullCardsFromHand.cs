using System;

public class BattlePhasePullCardsFromHand : IBattlePhase
{
    private readonly DeckInstance deck;
    private readonly int pullCardsNumber;
    private readonly LogicQueue logicQueue;
    

    public BattlePhasePullCardsFromHand(DeckInstance deck, int pullCardsNumber, LogicQueue logicQueue)
    {
        this.deck = deck;
        this.pullCardsNumber = pullCardsNumber;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        int count = pullCardsNumber;

        int? reshuffleAtIndex = null;
        if (count > deck.drawPile.Count)
        {
            reshuffleAtIndex = deck.drawPile.Count;
        }

        for (int i = 0; i < count; i++)
        {
            if (reshuffleAtIndex.HasValue && reshuffleAtIndex == i)
            {
                logicQueue.AddElement(0.5f, () => { deck.ReshuffleDeck(); });
            }
            logicQueue.AddElement(0.2f, () => { deck.DrawCard(); });
        }

        logicQueue.AddElement(0.1f, () =>
        {
            OnFinish?.Invoke();
        });
    }
}