using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CardQueueView))]
public class CardCommitAreaView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public Button cardCommitButton;
    [SerializeField] [SceneObjectsOnly] public CardQueueView cardQueueView;

    public event Action OnCommitClicked;

    private void Awake()
    {
        cardCommitButton.interactable = false;
    }

    public void AddCard(CardInstance card)
    {
        cardQueueView.AddCard(card);
        cardCommitButton.interactable = true;
    }

    private void Clear()
    {
        cardCommitButton.interactable = false;
        cardQueueView.Clear();
    }
}