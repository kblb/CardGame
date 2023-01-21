using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(CardQueueView))]
public class CardCommitAreaView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] private Button cardCommitButton;
    [SerializeField] [SceneObjectsOnly] private CardQueueView cardQueueView;

    private event Action OnCommitClicked;

    private void Awake()
    {
        cardCommitButton.onClick.AddListener(() => OnCommitClicked?.Invoke());
        cardCommitButton.interactable = false;
    }

    public void AddCard(CardInstance card)
    {
        cardQueueView.AddCard(card);
        cardCommitButton.interactable = true;
    }

    public void AddOnCommitListener(Action action)
    {
        OnCommitClicked += action;
    }

    private void Clear()
    {
        cardCommitButton.interactable = false;
        cardQueueView.Clear();
    }
}