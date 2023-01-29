using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DrawPileView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] private TMP_Text statusText;
    private int count;

    public void RemoveCard()
    {
        count--;
        statusText.text = $"Cards in draw pile: {count}";
    }

    public void Recount(int newCount)
    {
        count = newCount;
        statusText.text = $"Cards in draw pile: {count}";
    }
}