using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public ActorStatsView statsView;
    private GameObject model;
    public ActorInstance actorInstance;
    
    private float originalScale;
    private const float AttackScaleFactor = 1.2f;

    public event Action<ActorView> OnMouseOverEvent, OnMouseExitEvent;

    public void Init(ActorInstance actor)
    {
        actorInstance = actor;
        transform.localScale = Vector3.zero;
        statsView.Init(actor);
        model = Instantiate(actor.scriptableObject.prefab, transform);
        originalScale = model.transform.localScale.x;
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

    void OnMouseExit()
    {
        OnMouseExitEvent?.Invoke(this);
    }

    public void Highlight()
    {
        ShowAttackAnimation();
    }

    public void ResetHighlight()
    {
        HideAttackAnimation();
    }

    public void TurnOffHighlight()
    {
        model.transform.localScale /= AttackScaleFactor;
    }
}