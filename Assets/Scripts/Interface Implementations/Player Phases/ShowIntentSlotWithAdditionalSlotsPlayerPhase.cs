using System;

public class ShowIntentSlotWithAdditionalSlotsPlayerPhase : IPlayerPhase
{
    private readonly FightView fightView;
    private readonly DeckInstance playersDeck;
    public event Action OnCompleted;

    public ShowIntentSlotWithAdditionalSlotsPlayerPhase(FightView fightView, DeckInstance playersDeck)
    {
        this.fightView = fightView;
        this.playersDeck = playersDeck;

        fightView.uiView.cardCommitAreaView.OnCommitClicked += InvokeOnCompleted;
    }

    public void Start()
    {
        fightView.ShowIntentOverTarget(playersDeck.intent);
    }

    private void InvokeOnCompleted()
    {
        fightView.uiView.cardCommitAreaView.Hide();
        OnCompleted?.Invoke();
    }
}