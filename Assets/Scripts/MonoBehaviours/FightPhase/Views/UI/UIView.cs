using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] public CardCommitAreaView cardCommitAreaView;
    [SerializeField] [SceneObjectsOnly] public DiscardPileView discardPileView;
    [SerializeField] [SceneObjectsOnly] public DrawPileView drawPileView;
    [SerializeField] public HandView handView;

    [SerializeField] [AssetsOnly] public CardView cardViewPrefab;

    private readonly List<CardView> cardViews = new();

    public CardView CreateCardView(CardInstance cardInstance)
    {
        CardView cardView = Instantiate(cardViewPrefab, transform);
        cardView.Init(cardInstance);
        cardViews.Add(cardView);
        return cardView;
    }

    public void ShowHand(List<CardInstance> cardInstances)
    {
        IEnumerable<CardView> cardViewsInHand = cardViews.Where(t => cardInstances.Any(y => y == t.cardInstance));

        int i = 0;
        float offset = (cardViewsInHand.Count() - 1) * handView.spacing / 2;
        foreach (CardView cardView in cardViewsInHand)
        {
            cardView.transform.DOMove( handView.transform.position + new Vector3(i * handView.spacing - offset, 0, 0), 0.5f);
            i++;
        }
    }

    public void ShowCommitArea(List<CardInstance> intents)
    {
        IEnumerable<CardView> cardViewsInCommitArea = cardViews.Where(t => intents.Any(y => y == t.cardInstance));

        int i = 0;
        float offset = (cardViewsInCommitArea.Count() - 1) * handView.spacing / 2;
        foreach (CardView cardView in cardViewsInCommitArea)
        {
            cardView.transform.DOMove(cardCommitAreaView.transform.position + new Vector3(i * handView.spacing - offset, 0, 0), 0.5f);
            i++;
        }
    }

    public void ShowDiscardPile(List<CardInstance> discardPile)
    {
        IEnumerable<CardView> discardPileCards = cardViews.Where(t => discardPile.Any(y => y == t.cardInstance));

        int i = 0;
        float offset = (discardPileCards.Count() - 1) * handView.spacing / 2;
        foreach (CardView cardView in discardPileCards)
        {
            cardView.transform.DOMove(discardPileView.transform.position + new Vector3(i * handView.spacing - offset, 0, 0), 0.5f);
            i++;
        }
    }

    public void ShowDrawPile(List<CardInstance> drawPile)
    {
        IEnumerable<CardView> drawPileCards = cardViews.Where(t => drawPile.Any(y => y == t.cardInstance));

        int i = 0;
        float offset = (drawPileCards.Count() - 1) * handView.spacing / 2;
        foreach (CardView cardView in drawPileCards)
        {
            cardView.transform.DOMove(drawPileView.transform.position + new Vector3(i * handView.spacing - offset, 0, 0), 0.5f);
            i++;
        }
    }
}