using Sirenix.OdinInspector;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private float spacing = 30f;
    [SerializeField] [AssetsOnly] private CardView cardViewPrefab;

    public void AddCard(CardInstance card)
    {
        CardView cardView = Instantiate(cardViewPrefab, transform);
        cardView.Init(card);
        cardView.SetOnExitDragNotificationListener(Redraw);
        Redraw();
    }

    /// <summary>
    ///     Places all children in horizontal layout starting from the center
    /// </summary>
    private void Redraw()
    {
        int i = 0;
        int childCount = transform.childCount;
        float offset = (childCount - 1) * spacing / 2;
        foreach (Transform child in transform)
        {
            child.localPosition = new Vector3(i * spacing - offset, 0, 0);
            i++;
        }
    }
}