using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] public CardCommitAreaView cardCommitAreaView;
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

    public void ShowCommitArea(List<CardInstance> deckCommitArea)
    {
        IEnumerable<CardView> cardViewsInCommitArea = cardViews.Where(t => deckCommitArea.Any(y => y == t.cardInstance));

        int i = 0;
        float offset = (cardViewsInCommitArea.Count() - 1) * handView.spacing / 2;
        foreach (CardView cardView in cardViewsInCommitArea)
        {
            cardView.transform.DOMove(cardCommitAreaView.transform.position + new Vector3(i * handView.spacing - offset, 0, 0), 0.5f);
            i++;
        }
    }
}