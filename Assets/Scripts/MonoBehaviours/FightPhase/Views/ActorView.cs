using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] public ActorStatsView statsView;
    [SerializeField] private GameObject modelPrefab;
    private GameObject model;
    public FightPhaseActorInstance actorInstance;
    
    private float originalScale;
    private const float AttackScaleFactor = 1.2f;

    public void Init(FightPhaseActorInstance actor)
    {
        this.actorInstance = actor;
        transform.localScale = Vector3.zero;
        statsView.Init(actor);
        model = Instantiate(modelPrefab, transform);
        originalScale = model.transform.localScale.x;
        transform.DOScale(Vector3.one, 0.5f);
    }

    public void ShowAttackAnimation()
    {
        modelPrefab.transform.localScale = Vector3.one * (originalScale * AttackScaleFactor);
    }

    public void HideAttackAnimation()
    {
        modelPrefab.transform.localScale = Vector3.one * originalScale;
    }

    public void UpdateIntent(List<CardInstance> intents)
    {
        statsView.SetIntent(intents);
    }
}