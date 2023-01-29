using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DiscardPileView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] private TMP_Text statusText;
    private int count;

    public void AddCard()
    {
        count++;
        statusText.text = $"Discarded: {count}";
    }

    public void Zero()
    {
        count = 0;
        statusText.text = $"Discarded: {count}";
    }
}