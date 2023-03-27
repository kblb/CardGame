using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ActorView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public ActorStatsView statsView;
    [SerializeField] private Image hightlight;

    private GameObject model;
    private float originalScale;
    private const float AttackScaleFactor = 1.2f;

    public ActorInstance actorInstance;

    public event Action<ActorView> OnMouseOverEvent, OnMouseExitEvent, OnMouseEnteredEvent;

    public void Init(ActorInstance actor)
    {
        actorInstance = actor;
        transform.localScale = Vector3.zero;
        statsView.Init(actor);
        model = Instantiate(actor.scriptableObject.prefab, transform);
        originalScale = model.transform.localScale.x;
        hightlight.gameObject.SetActive(false);
        transform.DOScale(Vector3.one, 0.5f);
    }

    public void ShowAttackAnimation()
    {
        model.transform.localScale *= AttackScaleFactor;
    }

    public void HideAttackAnimation()
    {
        model.transform.localScale = Vector3.one * originalScale;
    }

    public void UpdateIntent(IntentInstance intent)
    {
        statsView.UpdateIntent(intent);
    }

    void OnMouseOver()
    {
        OnMouseOverEvent?.Invoke(this);
    }

    private void OnMouseEnter()
    {
        OnMouseEnteredEvent?.Invoke(this);
    }

    void OnMouseExit()
    {
        OnMouseExitEvent?.Invoke(this);
    }

    public void Highlight()
    {
        hightlight.gameObject.SetActive(true);
    }

    public void TurnOffHighlight()
    {
        hightlight.gameObject.SetActive(false);
    }

    public void Selected()
    {
        hightlight.color = Color.red;
    }

    public void TurnOffSelect()
    {
        hightlight.color = Color.green;
    }
}