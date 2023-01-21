using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

/**
 * This view is used to show
 * - player hand
 * - commit area
 */
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class CardQueueView : MonoBehaviour
{
    [SerializeField] [AssetsOnly] private CardSmallView cardPrefab;

    private readonly List<CardSmallView> _cardViews = new();

    public void AddCard(CardInstance cardInstance)
    {
        CardSmallView cardView = Instantiate(cardPrefab, transform);
        cardView.SetIcon(cardInstance.scriptableObject.icon);
        _cardViews.Add(cardView);
    }

    public void Clear()
    {
        foreach (CardSmallView cardView in _cardViews)
        {
            Destroy(cardView.gameObject);
        }

        _cardViews.Clear();
    }
}